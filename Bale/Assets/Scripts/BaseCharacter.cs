using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public bool IsCasting { get; protected set; }
    [SerializeField]
    protected ContainerComponent m_lifeComponent;
    [SerializeField]
    protected ContainerComponent m_manaComponent;
    [SerializeField]
    protected CastingBarUI m_castingBar;
    [SerializeField]
    protected List<AbilityData> m_abilitiesList = new List<AbilityData>();
    [SerializeField]
    protected EquippedAbilities m_equippedAbilities;
    protected AbilityButton m_currentAbilityCast;
    [SerializeField]
    protected Dictionary<AbilityData, float> m_buffsList = new Dictionary<AbilityData, float>();
    [SerializeField]
    protected Dictionary<AbilityData, float> m_debuffsList = new Dictionary<AbilityData, float>();
    [SerializeField]
    protected BoonsPanel m_buffsPanel;
    [SerializeField]
    protected BoonsPanel m_debuffsPanel;

    private void Start()
    {
        m_lifeComponent.m_resourceEmptyCallback += OnHealthEmpty;
        m_equippedAbilities?.InstantiateAbilities(m_abilitiesList);
    }

    private void Update()
    {
        if (IsCasting)
        {
            m_castingBar.UpdateUI(m_currentAbilityCast.CurrentCast, m_currentAbilityCast.Data.CastingTime);
        }
    }

    public void OnTryCast(AbilityButton abilityButton)
    {
        if (!CanCast(abilityButton))
        {
            return;
        }
        m_manaComponent.RemoveResource(abilityButton.Data.ManaCost);
        IsCasting = true;
        abilityButton.BeginCast();
        m_currentAbilityCast = abilityButton;
        m_castingBar.OnBeginCast(abilityButton.Data.AbilityType.ToString(), abilityButton.Data.Sprite);
    }

    protected bool CanCast(AbilityButton ability)
    {
        return !ability.InCooldown && !IsCasting && m_manaComponent.CurrentValue >= ability.Data.ManaCost && ability.Data.AbilityType == EAbilityType.Active;
    }

    public void OnEndCast()
    {
        AbilitySystem.CastAbility(this, m_currentAbilityCast.Data);

        IsCasting = false;
        m_currentAbilityCast = null;
        m_castingBar.OnEndCast();
    }

    public void ReceiveDamage(BaseCharacter damageSource, float amount)
    {
        m_lifeComponent.RemoveResource(amount);
    }

    public void HealDamage(float amount)
    {
        m_lifeComponent.GainResource(amount);
    }

    public void GainMana(float amount)
    {
        m_manaComponent.GainResource(amount);
    }

    protected void OnHealthEmpty()
    {
        Debug.Log("Game won!!");
    }

    public void OnEquipAbility(AbilityData abilityData)
    {
        if (abilityData.AbilityType == EAbilityType.Passive)
        {
            GainBuff(abilityData);
        }
    }

    public void OnUnequipAbility(AbilityData abilityData)
    {
        if (m_buffsList.ContainsKey(abilityData))
        {
            RemoveBuff(abilityData);
        }
    }

    protected void GainBuff(AbilityData abilityData, int stacks = 1)
    {
        m_buffsList.Add(abilityData, -1.0f);
        m_buffsPanel.SetBoon(abilityData, stacks);
    }

    protected void RemoveBuff(AbilityData abilityData)
    {
        if (m_buffsList.ContainsKey(abilityData))
        {
            m_buffsList.Remove(abilityData);
            m_buffsPanel.RemoveBoon(abilityData);
        }
    }

    public void GainDebuff(AbilityData abilityData, int stacks = 1, float duration = -1.0f)
    {
        m_debuffsList.Add(abilityData, duration);
        m_debuffsPanel.SetBoon(abilityData, stacks);
    }

    protected void RemoveDebuff(AbilityData abilityData)
    {
        if (m_debuffsList.ContainsKey(abilityData))
        {
            m_debuffsList.Remove(abilityData);
            m_debuffsPanel.RemoveBoon(abilityData);
        }
    }

    public void OnTickEvent()
    {
        var buffsToRemove = new List<AbilityData>();
        var buffKeys = m_buffsList.Keys.ToArray();

        foreach (var buffKey in buffKeys)
        {
            if (buffKey.AbilityType == EAbilityType.Passive)
            {
                ApplyOnTickAbility(buffKey);
            }
            else
            {
                ApplyOnTickAbility(buffKey);
                m_buffsList[buffKey] -= GameManager.TICK_RATE;
                if (m_buffsList[buffKey] <= 0.0f)
                {
                    buffsToRemove.Add(buffKey);
                }
            }
        }
        foreach (var buffToRemove in buffsToRemove)
        {
            RemoveBuff(buffToRemove);
        }

        var debuffsToRemove = new List<AbilityData>();
        var debuffKeys = m_debuffsList.Keys.ToArray();
        foreach (var debuffKey in debuffKeys)
        {
            if (debuffKey.AbilityType == EAbilityType.Passive)
            {
                ApplyOnTickAbility(debuffKey);
            }
            else if (debuffKey.AbilityType == EAbilityType.Active)
            {
                ApplyOnTickAbility(debuffKey);
                m_debuffsList[debuffKey] -= GameManager.TICK_RATE;
                if (m_debuffsList[debuffKey] <= 0.0f)
                {
                    debuffsToRemove.Add(debuffKey);
                }
            }
        }
        foreach (var debuffToRemove in debuffsToRemove)
        {
            RemoveDebuff(debuffToRemove);
        }
    }

    protected void ApplyOnTickAbility(AbilityData abilityData)
    {
        foreach (var effect in abilityData.AbilityEffects)
        {
            ApplyOnTickEffect(effect);
        }
    }

    protected void ApplyOnTickEffect(SAbilityEffect effect)
    {
        switch (effect.Effect)
        {
            case EAbilityEffect.HealthRegenBuff:
                HealDamage(effect.Magnitude * GameManager.TICK_RATE);
                break;
            case EAbilityEffect.ManaRegenBuff:
                GainMana(effect.Magnitude * GameManager.TICK_RATE);
                break;
            case EAbilityEffect.BleedDebuff:
                ReceiveDamage(this, (float)effect.Magnitude * GameManager.TICK_RATE);
                break;
        }
    }
}
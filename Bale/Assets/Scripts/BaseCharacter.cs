using System.Collections.Generic;
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

    public void ReceiveDamage(BaseCharacter damageSource, int amount)
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
            m_buffsList.Add(abilityData, -1.0f);
        }
    }

    public void OnUnequipAbility(AbilityData abilityData)
    {
        if (m_buffsList.ContainsKey(abilityData))
        {
            m_buffsList.Remove(abilityData);
        }
    }

    public void OnTickEvent()
    {
        foreach (var buff in m_buffsList)
        {
            if (buff.Key.AbilityType == EAbilityType.Passive)
            {
                ApplyOnTickAbility(buff.Key);
            }
            else
            {
                //TODO: buff.Value -= GameManager.TICK_RATE;
            }
        }
        foreach (var debuff in m_debuffsList)
        {
            if (debuff.Key.AbilityType == EAbilityType.Passive)
            {
                ApplyOnTickAbility(debuff.Key);
            }
            else
            {
                //TODO: buff.Value -= GameManager.TICK_RATE;
            }
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
        }
    }
}
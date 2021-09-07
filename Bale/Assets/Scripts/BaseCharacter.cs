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

    private void Start()
    {
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
        abilityButton.Cast();
        m_currentAbilityCast = abilityButton;
        m_castingBar.OnBeginCast(abilityButton.Data.AbilityType.ToString(), abilityButton.Data.Sprite);
    }

    protected bool CanCast(AbilityButton ability)
    {
        return !ability.InCooldown && !IsCasting && m_manaComponent.CurrentValue >= ability.Data.ManaCost;
    }

    public void OnEndCast()
    {
        IsCasting = false;
        m_currentAbilityCast = null;
        m_castingBar.OnEndCast();
    }
}

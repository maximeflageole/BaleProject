using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
public class AbilityData : ScriptableObject
{
    public EAbility Ability = EAbility.Count;
    public EAbilityType AbilityType = EAbilityType.Active;
    public EStackType EStackType = EStackType.RefreshDuration;
    public int ManaCost = 5;
    public Sprite Sprite;
    public float Cooldown = 10.0f;
    public float CastingTime = 2.0f;
    public List<SAbilityEffect> AbilityEffects = new List<SAbilityEffect>();
}

public enum EAbility
{
    Test,
    Test2,
    Test3,
    InstantKill,
    SelfDamage,
    SelfHeal,
    HealEveryone,
    Suicide,
    ManaSignet,
    HealthSignet,
    SevereArtery,
    LowDamage,
    Count
}

public enum EAbilityType
{
    Active,
    Passive,
    Toggle,
    Count
}

public enum EStackType
{
    None,
    RefreshDuration,
    StacksDuration,
    StacksIntensity,
    StacksIntensityAndRefreshDuration,
    Count
}
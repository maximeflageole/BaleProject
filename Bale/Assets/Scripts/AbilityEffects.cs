using System.Collections.Generic;

public class AbilitySystem
{
    public static void CastAbility(BaseCharacter castingCharacter, AbilityData data)
    {
        foreach (var effect in data.AbilityEffects)
        {
            CastEffect(castingCharacter, effect);
        }
    }

    public static void CastEffect(BaseCharacter castingCharacter, SAbilityEffect effect)
    {
        var targetList = GameManager._Instance.GetAbilityEffectTarget(castingCharacter, effect);

        switch (effect.Effect)
        {
            case EAbilityEffect.Damage:
                AbilitySystem.DamageCharacters(castingCharacter, targetList, effect.Magnitude);
                break;
            case EAbilityEffect.Heal:
                AbilitySystem.HealCharacters(targetList, effect.Magnitude);
                break;
            case EAbilityEffect.RemoveMana:
                break;
            case EAbilityEffect.GainMana:
                break;
        }
    }

    public static void DamageCharacters(BaseCharacter damagingCharacter, List<BaseCharacter> targets, int amount)
    {
        if (targets.Count == 0)
            return;
        foreach (var victim in targets)
        {
            victim.ReceiveDamage(damagingCharacter, amount);
        }
    }

    public static void HealCharacters(List<BaseCharacter> targets, int amount)
    {
        foreach (var character in targets)
        {
            character.HealDamage(amount);
        }
    }
}

[System.Serializable]
public struct SAbilityEffect
{
    public EAbilityEffect Effect;
    public EAbilityTarget Targets;
    public int Magnitude;
    public float Duration;
}

public enum EAbilityEffect
{
    Damage,
    Heal,
    RemoveMana,
    GainMana,
    ManaRegenBuff,
    HealthRegenBuff,
    Count
}

public enum EAbilityTarget
{
    Self,
    Enemy,
    AllEnemies,
    Ally,
    AllyOrSelf,
    Everyone,
    EveryoneButSelf,
    Count
}
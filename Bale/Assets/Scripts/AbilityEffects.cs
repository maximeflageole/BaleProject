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
        switch (effect.Effect)
        {
            case EAbilityEffect.Damage:
                AbilitySystem.DamageCharacter(castingCharacter, GameManager._Instance.GetFacingEnemy(castingCharacter), effect.Magnitude);
                break;
            case EAbilityEffect.Heal:
                AbilitySystem.HealCharacter(castingCharacter, effect.Magnitude);
                break;
            case EAbilityEffect.RemoveMana:
                break;
            case EAbilityEffect.GainMana:
                break;
        }
    }

    public static void DamageCharacter(BaseCharacter damagingCharacter, BaseCharacter victimCharacter, int amount)
    {
        victimCharacter.ReceiveDamage(damagingCharacter, amount);
    }

    public static void HealCharacter(BaseCharacter character, int amount)
    {
        character.HealDamage(amount);
    }
}

[System.Serializable]
public struct SAbilityEffect
{
    public EAbilityEffect Effect;
    public int Magnitude;
}

public enum EAbilityEffect
{
    Damage,
    Heal,
    RemoveMana,
    GainMana,
    Count
}
public class CharacterAI : BaseCharacter
{
    protected int m_currentAbilityIndex = 0;

    protected override void Update()
    {
        base.Update();
        if (!IsCasting)
        {
            m_equippedAbilities.CastDefaultAbility();
        }
    }

    protected override bool CanCast(AbilitySlot ability)
    {
        return !ability.InCooldown && !IsCasting && ability.Data.AbilityType == EAbilityType.Active;
    }
}
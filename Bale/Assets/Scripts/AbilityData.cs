using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "ScriptableObjects/AbilityData")]
public class AbilityData : ScriptableObject
{
    public EAbility AbilityType = EAbility.Count;
    public Sprite Sprite;
    public float Cooldown = 10.0f;
    public float CastingTime = 2.0f;
}

public enum EAbility
{
    Test,
    Test2,
    Test3,
    Count
}
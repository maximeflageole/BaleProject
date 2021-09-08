using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;

    [SerializeField]
    private BaseCharacter m_playerCharacter;
    [SerializeField]
    private BaseCharacter m_enemy;

    private void Awake()
    {
        if (_Instance == null)
        {
            _Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }

        Destroy(gameObject);
    }

    public BaseCharacter GetFacingEnemy(BaseCharacter character)
    {
        if (character == m_playerCharacter)
        {
            return m_enemy;
        }
        return m_playerCharacter;
    }

    public List<BaseCharacter> GetAbilityEffectTarget(BaseCharacter castingCharacter, SAbilityEffect effect)
    {
        var characters = new List<BaseCharacter>();
        switch (effect.Targets)
        {
            case EAbilityTarget.Self:
                characters.Add(castingCharacter);
                break;
            case EAbilityTarget.Enemy:
                characters.Add(GetFacingEnemy(castingCharacter));
                break;
            case EAbilityTarget.Everyone:
                characters.Add(castingCharacter);
                characters.Add(GetFacingEnemy(castingCharacter));
                break;
        }
        return characters;
    }
}

using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Instance;
    public const float TICK_RATE = 0.25f;
    public static float CURRENT_TICK = 0.0f;

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

    private void Update()
    {
        CURRENT_TICK += Time.deltaTime;
        if (CURRENT_TICK >= TICK_RATE)
        {
            CURRENT_TICK %= TICK_RATE;
            OnTickEvent();
        }
    }

    private void OnTickEvent()
    {
        m_playerCharacter.OnTickEvent();
        m_enemy.OnTickEvent();
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

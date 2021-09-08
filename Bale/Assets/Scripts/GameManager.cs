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
}

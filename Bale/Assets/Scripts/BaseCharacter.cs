using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected ContainerComponent m_lifeComponent;
    [SerializeField]
    protected List<AbilityData> m_abilitiesList = new List<AbilityData>();
    [SerializeField]
    protected EquippedAbilities m_equippedAbilities;

    // Start is called before the first frame update
    void Awake()
    {
        m_lifeComponent = GetComponent<ContainerComponent>();
    }

    private void Start()
    {
        m_equippedAbilities?.InstantiateAbilities(m_abilitiesList);
    }
}

using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    [SerializeField]
    protected ContainerComponent m_lifeComponent;
    [SerializeField]
    protected ContainerComponent m_manaComponent;
    [SerializeField]
    protected List<AbilityData> m_abilitiesList = new List<AbilityData>();
    [SerializeField]
    protected EquippedAbilities m_equippedAbilities;

    private void Start()
    {
        m_equippedAbilities?.InstantiateAbilities(m_abilitiesList);
    }
}

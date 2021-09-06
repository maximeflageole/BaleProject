using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquippedAbilities : MonoBehaviour
{
    protected List<AbilityButton> m_abilitiesButtons = new List<AbilityButton>();
    [SerializeField]
    protected List<string> m_buttonsShortcuts = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        var abilitiesButtons = GetComponentsInChildren<AbilityButton>();
        m_abilitiesButtons = abilitiesButtons.ToList();
        for (var i = 0; i < m_abilitiesButtons.Count; i++)
        {
            m_abilitiesButtons[i].InstantiateButtonShortcut(m_buttonsShortcuts[i]);
        }
    }

    public void InstantiateAbilities(List<AbilityData> abilitiesData)
    {
        var abilitiesButtons = GetComponentsInChildren<AbilityButton>();
        m_abilitiesButtons = abilitiesButtons.ToList();

        for (var i = 0; i < m_abilitiesButtons.Count || i < abilitiesData.Count; i++)
        {
            m_abilitiesButtons[i].SetAbilityData(abilitiesData[i]);
        }
    }
}
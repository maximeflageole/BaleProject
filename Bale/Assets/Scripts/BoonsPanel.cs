using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoonsPanel : MonoBehaviour
{
    [SerializeField]
    protected List<BoonUI> m_availableBoonsUI = new List<BoonUI>();
    protected Dictionary<AbilityData, BoonUI> m_currentBoonsUI = new Dictionary<AbilityData, BoonUI>();

    public void SetBoon(AbilityData abilityData, int stacks)
    {
        if (m_currentBoonsUI.ContainsKey(abilityData))
        {
            m_currentBoonsUI[abilityData].SetAbilityData(abilityData, stacks);
        }
        else
        {
            var boonUi = m_availableBoonsUI[0];
            m_availableBoonsUI.RemoveAt(0);
            boonUi.SetAbilityData(abilityData, stacks);
            m_currentBoonsUI.Add(abilityData, boonUi);
        }
    }

    public void RemoveBoon(AbilityData abilityData)
    {
        if (m_currentBoonsUI.ContainsKey(abilityData))
        {
            var boonUi = m_currentBoonsUI[abilityData];
            boonUi.RemoveAbilityData();
            m_currentBoonsUI.Remove(abilityData);
            m_availableBoonsUI.Add(boonUi);
        }
    }
}

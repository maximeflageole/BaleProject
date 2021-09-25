using System.Collections.Generic;
using UnityEngine;

public class EquippedAbilities : MonoBehaviour
{
    [SerializeField]
    protected BaseCharacter m_owner;
    public List<AbilitySlot> AbilitiesList { get; protected set; } = new List<AbilitySlot>();

    public void InstantiateAbilities(List<AbilityData> abilitiesData)
    {
        AbilitiesList.Clear();
        var abilitiesSlots = GetComponentsInChildren<AbilitySlot>();

        var i = 0;
        foreach (var slot in abilitiesSlots)
        {
            if (i >= abilitiesData.Count)
            {
                return;
            }
            AbilitiesList.Add(slot);
            var btn = slot.GetComponent<AbilityButton>();
            if (btn != null)
            {
                btn.InstantiateButton(i.ToString(), m_owner);
            }
            else
            {
                slot.InstantiateSlot(m_owner);
            }
            abilitiesSlots[i].SetAbilityData(abilitiesData[i]);
            i++;
        }
    }

    public void CastDefaultAbility()
    {
        m_owner.OnTryCast(AbilitiesList[0]);
    }
}
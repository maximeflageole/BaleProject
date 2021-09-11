using UnityEngine;
using UnityEngine.UI;

public class BoonUI : MonoBehaviour
{
    public int Stacks { get; protected set; }
    public AbilityData AbilityData { get; protected set; }
    [SerializeField]
    protected Image m_image;

    public void SetAbilityData(AbilityData abilityData, int stacks)
    {
        AbilityData = abilityData;
        Stacks = stacks;
        m_image.sprite = abilityData.Sprite;
        gameObject.SetActive(true);
    }

    public void RemoveAbilityData()
    {
        Stacks = 0;
        AbilityData = null;
        m_image.sprite = null;
        gameObject.SetActive(false);
    }
}

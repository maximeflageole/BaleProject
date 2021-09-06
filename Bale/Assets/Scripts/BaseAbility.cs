using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    [SerializeField]
    protected AbilityData m_abilityData;
    
    // Start is called before the first frame update
    public void SetAbilityData(AbilityData data)
    {
        m_abilityData = data;
    }
}

using UnityEngine;

public class ContainerComponent : MonoBehaviour
{
    [SerializeField]
    protected float m_maxValue = 100.0f;
    [field: SerializeField]
    public float CurrentValue { get; protected set; } = 100.0f;
    [SerializeField]
    protected float m_regenPer10 = 10.0f;
    [SerializeField]
    protected ContainerUI m_containerUI;

    // Update is called once per frame
    void Update()
    {
        m_containerUI?.UpdateUI(CurrentValue, m_maxValue);
        CurrentValue += m_regenPer10 * Time.deltaTime * 0.1f;
        CurrentValue = Mathf.Clamp(CurrentValue, 0.0f, m_maxValue);
    }

    public void RemoveResource(float amount)
    {
        CurrentValue -= amount;
        CurrentValue = Mathf.Clamp(CurrentValue, 0.0f, m_maxValue);
    }
}
using UnityEngine;

public class ContainerComponent : MonoBehaviour
{
    [SerializeField]
    protected float m_maxValue = 100.0f;
    [SerializeField]
    protected float m_currentValue = 100.0f;
    [SerializeField]
    protected float m_regenPer10 = 10.0f;
    [SerializeField]
    protected ContainerUI m_containerUI;

    // Update is called once per frame
    void Update()
    {
        m_containerUI?.UpdateUI(m_currentValue, m_maxValue);
        m_currentValue += m_regenPer10 * Time.deltaTime * 0.1f;
        m_currentValue = Mathf.Clamp(m_currentValue, 0.0f, m_maxValue);
    }
}
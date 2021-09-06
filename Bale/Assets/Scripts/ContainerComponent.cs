using UnityEngine;

public class ContainerComponent : MonoBehaviour
{
    [SerializeField]
    protected float m_maxValue = 100.0f;
    [SerializeField]
    protected float m_currentValue = 100.0f;
    [SerializeField]
    protected ContainerUI m_lifeBarUI;

    // Update is called once per frame
    void Update()
    {
        m_lifeBarUI.UpdateUI(m_currentValue / m_maxValue);
    }
}
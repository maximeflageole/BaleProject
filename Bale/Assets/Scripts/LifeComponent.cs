using UnityEngine;

public class LifeComponent : MonoBehaviour
{
    [SerializeField]
    protected float m_maxValue = 100.0f;
    [SerializeField]
    protected float m_currentValue = 100.0f;
    [SerializeField]
    protected LifeBarUI m_lifeBarUI;

    // Update is called once per frame
    void Update()
    {
        m_lifeBarUI.UpdateLifeUI(m_currentValue / m_maxValue);
    }
}
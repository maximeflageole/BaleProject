using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField]
    protected float m_cooldown = 3.0f;
    protected float m_currentCooldown;
    protected bool m_inCooldown;

    [SerializeField]
    protected Image m_cooldownImage;

    public void OnTryCast()
    {
        if (!m_inCooldown)
        {
            Cast();
        }
    }

    private void Cast()
    {
        m_inCooldown = true;
        m_currentCooldown = m_cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_inCooldown)
        {
            m_currentCooldown -= Time.deltaTime;
            if (m_currentCooldown <= 0.0f)
            {
                m_inCooldown = false;
                m_currentCooldown = Mathf.Max(m_currentCooldown, 0.0f);
            }
        }
        m_cooldownImage.fillAmount = m_currentCooldown / m_cooldown;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField]
    protected AbilityData m_data;

    protected float m_currentCooldown;
    protected bool m_inCooldown;


    [SerializeField]
    protected Image m_abilityImage;
    [SerializeField]
    protected Image m_cooldownImage;

    void Start()
    {
        m_abilityImage.sprite = m_data.Sprite;
    }

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
        m_currentCooldown = m_data.Cooldown;
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
        m_cooldownImage.fillAmount = m_currentCooldown / m_data.Cooldown;
    }
}

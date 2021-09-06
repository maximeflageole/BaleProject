using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    [SerializeField]
    protected AbilityData m_data;

    protected float m_currentCooldown;
    protected bool m_inCooldown;
    protected string m_keyboardShortcut;

    [SerializeField]
    protected TextMeshProUGUI m_tmproShortcut;
    [SerializeField]
    protected Image m_abilityImage;
    [SerializeField]
    protected Image m_cooldownImage;

    void Awake()
    {
        m_abilityImage.sprite = m_data.Sprite;
    }

    public void SetAbilityData(AbilityData data)
    {
        m_data = data;
        m_currentCooldown = m_data.Cooldown;
        m_inCooldown = true;
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

        if (Input.GetKeyDown(m_keyboardShortcut))
        {
            OnTryCast();
        }
    }

    public void InstantiateButtonShortcut(string keyname)
    {
        m_keyboardShortcut = keyname;
        m_tmproShortcut.text = keyname;
    }
}

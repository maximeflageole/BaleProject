using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    protected BaseCharacter m_owner;
    [field:SerializeField]
    public AbilityData Data { get; protected set; }

    protected float m_currentCooldown;
    public float CurrentCast { get; protected set; }
    public bool InCooldown { get; protected set; }
    public bool IsCasting { get; protected set; }
    protected string m_keyboardShortcut = "2";

    [SerializeField]
    protected TextMeshProUGUI m_tmproShortcut;
    [SerializeField]
    protected Image m_abilityImage;
    [SerializeField]
    protected Image m_cooldownImage;

    void Awake()
    {
        m_abilityImage.sprite = Data.Sprite;
    }

    public void SetAbilityData(AbilityData data)
    {
        Data = data;
        m_currentCooldown = Data.Cooldown;
        InCooldown = true;
        m_abilityImage.sprite = Data.Sprite;
    }

    public void OnTryCast()
    {
        if (!InCooldown)
        {
            m_owner.OnTryCast(this);
        }
    }

    public void Cast()
    {
        IsCasting = true;
        CurrentCast = 0.0f;
        InCooldown = true;
        m_currentCooldown = Data.Cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCasting)
        {
            CurrentCast += Time.deltaTime;
            if (CurrentCast >= Data.CastingTime)
            {
                IsCasting = false;
                CurrentCast = 0.0f;
                m_owner.OnEndCast();
            }
        }
        else if (InCooldown)
        {
            m_currentCooldown -= Time.deltaTime;
            if (m_currentCooldown <= 0.0f)
            {
                InCooldown = false;
                m_currentCooldown = Mathf.Max(m_currentCooldown, 0.0f);
            }
        }
        m_cooldownImage.fillAmount = m_currentCooldown / Data.Cooldown;

        if (Input.GetKeyDown(m_keyboardShortcut))
        {
            m_owner.OnTryCast(this);
        }
    }

    public void InstantiateButtonShortcut(string keyname, BaseCharacter owner)
    {
        m_keyboardShortcut = keyname;
        m_tmproShortcut.text = keyname;
        m_owner = owner;
    }
}

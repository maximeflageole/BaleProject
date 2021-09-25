using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : AbilitySlot
{
    protected string m_keyboardShortcut = "2";

    [SerializeField]
    protected TextMeshProUGUI m_tmproShortcut;
    [SerializeField]
    protected Image m_abilityImage;
    [SerializeField]
    protected Image m_cooldownImage;

    public override void SetAbilityData(AbilityData data)
    {
        base.SetAbilityData(data);
        m_abilityImage.sprite = Data.Sprite;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Data.AbilityType == EAbilityType.Passive)
        {
            m_cooldownImage.fillAmount = 0.0f;
        }
        else
        {
            m_cooldownImage.fillAmount = m_currentCooldown / Data.Cooldown;
        }

        if (Input.GetKeyDown(m_keyboardShortcut))
        {
            m_owner.OnTryCast(this);
        }
    }

    public void InstantiateButton(string keyname, BaseCharacter owner)
    {
        base.InstantiateSlot(owner);

        if (owner == GameManager._Instance.PlayerCharacter)
        {
            m_keyboardShortcut = keyname;
            m_tmproShortcut.text = keyname;
        }
    }
}

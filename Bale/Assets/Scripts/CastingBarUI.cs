using UnityEngine;
using UnityEngine.UI;

public class CastingBarUI : ContainerUI
{
    [SerializeField]
    protected Image m_abilityImage;

    public void OnBeginCast(string abilityName, Sprite abilitySprite)
    {
        m_abilityImage.sprite = abilitySprite;
        m_containerText.text = abilityName;
        gameObject.SetActive(true);
    }

    public void OnEndCast()
    {
        gameObject.SetActive(false);
    }

    public override void UpdateUI(float currentValue, float maxValue)
    {
        m_uiImage.fillAmount = currentValue / maxValue;
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    [SerializeField]
    protected Image m_uiImage;
    [SerializeField]
    protected TextMeshProUGUI m_containerText;

    public virtual void UpdateUI(float currentValue, float maxValue)
    {
        m_uiImage.fillAmount = currentValue / maxValue;
        var roundedDownValue = Mathf.FloorToInt(currentValue);
        if (m_containerText != null)
            m_containerText.text = roundedDownValue.ToString() + " / " + maxValue.ToString("0");
    }
}
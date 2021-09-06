using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    [SerializeField]
    protected Image m_uiImage;
    [SerializeField]
    protected TextMeshProUGUI m_containerText;

    public void UpdateUI(float currentValue, float maxValue)
    {
        m_uiImage.fillAmount = currentValue / maxValue;
        if (m_containerText != null)
            m_containerText.text = currentValue.ToString("0") + " / " + maxValue.ToString("0");
    }
}
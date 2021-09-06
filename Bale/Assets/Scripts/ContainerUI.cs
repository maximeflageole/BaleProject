using UnityEngine;
using UnityEngine.UI;

public class ContainerUI : MonoBehaviour
{
    [SerializeField]
    protected Image m_uiImage;

    public void UpdateUI(float percentage)
    {
        m_uiImage.fillAmount = percentage;
    }
}

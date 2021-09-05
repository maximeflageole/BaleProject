using UnityEngine;
using UnityEngine.UI;

public class LifeBarUI : MonoBehaviour
{
    [SerializeField]
    protected Image m_lifeImage;

    public void UpdateLifeUI(float percentage)
    {
        m_lifeImage.fillAmount = percentage;
    }
}

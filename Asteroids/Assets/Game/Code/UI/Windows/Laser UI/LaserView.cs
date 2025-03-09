using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LaserUI
{
    public sealed class LaserView : MonoBehaviour
    {
        public TextMeshProUGUI CountLaserText;
        public Image FillImage;

        public void SetFillImage(float percent)
        {
            FillImage.fillAmount = percent;
        }
    }
}

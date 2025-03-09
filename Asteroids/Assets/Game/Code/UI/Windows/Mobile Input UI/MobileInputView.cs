using UnityEngine;
using UnityEngine.UI;

namespace MobileInputUI
{
    public class MobileInputView : MonoBehaviour
    {
        [SerializeField] private Button _bulletButton;
        [SerializeField] private Button _laserButton;
        [SerializeField] private Transform _joystick;
        [SerializeField] private RectTransform _joystickFill;
        [SerializeField] private CanvasGroup _joystickCanvasGroup;

        public void SetActionBulletButton(System.Action action)
        {
            _bulletButton.onClick.AddListener(() => action?.Invoke());
        }

        public void SetActionLaserButton(System.Action action)
        {
            _laserButton.onClick.AddListener(() => action?.Invoke());
        }

        public void ResetButtons()
        {
            _bulletButton.onClick.RemoveAllListeners();
            _laserButton.onClick.RemoveAllListeners();
        }

        public void SetJoystickPosition(Vector3 position)
        {
            _joystick.position = position;
        }

        public void SetJoystickFillDirection(Vector3 direction, float multiply)
        {
            _joystickFill.anchoredPosition = direction * multiply;
        }

        public void SetJoystickAlpha(float alpha)
        {
            _joystickCanvasGroup.alpha = alpha;
        }
    }
}

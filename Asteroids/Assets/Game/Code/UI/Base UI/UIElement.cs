using UnityEngine;

namespace BaseUI
{
    public class UIElement : MonoBehaviour
    {
        [SerializeField] private string _key = "ui";

        private bool _isShow = false;

        public string Key => _key;
        public bool IsShow => _isShow;

        public void Show()
        {
            _isShow = true;
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            _isShow = false;
            gameObject.SetActive(false);
        }
    }
}

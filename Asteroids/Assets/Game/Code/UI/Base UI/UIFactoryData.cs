using FactoryService;
using UnityEngine;

namespace BaseUI
{
    [System.Serializable]
    public sealed class UIFactoryData : FactoryData<UIElement>
    {
        [SerializeField] private Canvas _canvas;

        public UIFactoryData()
        {
            onInstantiate = OnInstantiate;
            onGet = OnGet;
            onReturn = OnReturn;
        }

        private void OnInstantiate(UIElement ui)
        {
            if (ui != null)
            {
                ui.Hide();
            }
        }

        private void OnGet(UIElement ui, Vector3 position)
        {
            if (ui != null)
            {
                ui.transform.SetParent(_canvas.transform);
                UIStretchToFullScreen(ui);
            }
        }

        private void OnReturn(UIElement ui)
        {
            if (ui != null)
            {
                ui.Hide();
            }
        }

        private void UIStretchToFullScreen(UIElement ui)
        {
            RectTransform rectTransform = ui.GetComponent<RectTransform>();
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            Canvas.ForceUpdateCanvases();
        }
    }
}

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnitUI
{
    public sealed class UnitView : MonoBehaviour
    {
        [SerializeField] private Transform _healthContent;
        [SerializeField] private Image _healthPrefab;
        [SerializeField] private Sprite _enableHealthSprite;
        [SerializeField] private Sprite _disableHealthSprite;

        private List<Image> _healthImages = new List<Image>();

        public List<Image> HealthImages => _healthImages;

        public TextMeshProUGUI PositionText;
        public TextMeshProUGUI AngleText;
        public TextMeshProUGUI SpeedText;

        public void CreateHealthImage()
        {
            Image heart = Instantiate(_healthPrefab, _healthContent);
            heart.sprite = _enableHealthSprite;
            _healthImages.Add(heart);
        }

        public void SetHealthImageSprite(Image heart, bool isEmpty)
        {
            heart.sprite = isEmpty ? _disableHealthSprite : _enableHealthSprite;
        }
    }
}

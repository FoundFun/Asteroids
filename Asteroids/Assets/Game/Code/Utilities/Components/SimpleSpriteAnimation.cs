using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class SimpleSpriteAnimation : MonoBehaviour
    {
        [SerializeField] private float _frame = 0.2f;
        [SerializeField] private List<Sprite> _sprites = new List<Sprite>();

        private int _indexSprite = 0;
        private SpriteRenderer _spriteRenderer;
        private AsyncMethod _asyncMethod;

        private void Awake()
        {
            _asyncMethod = new AsyncMethod(_frame, false, null, UpdateSprite, null);
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _asyncMethod.Run();
        }

        private void OnDestroy()
        {
            _asyncMethod.Stop();
        }

        private void UpdateSprite()
        {
            if (gameObject != null && gameObject.activeInHierarchy == false) return;

            _indexSprite++;

            if (_indexSprite >= _sprites.Count)
                _indexSprite = 0;

            _spriteRenderer.sprite = _sprites[_indexSprite];
        }
    }
}

using UnityEngine;

namespace FriendlyMonster.RhubarbTimeline
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class RhubarbSprite : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private RhubarbSpriteSet _rhubarbSpriteSet;

        public RhubarbSpriteSet RhubarbSpriteSet
        {
            get { return _rhubarbSpriteSet; }
            set { _rhubarbSpriteSet = value; }
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public MouthShape MouthShape
        {
            set
            {
                if (_spriteRenderer == null)
                {
                    _spriteRenderer = GetComponent<SpriteRenderer>();
                }

                _spriteRenderer.sprite = _rhubarbSpriteSet.GetSprite(value);
            }
        }
    }
}
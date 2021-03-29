using BlastGame.Core.Controllers;
using BlastGame.Core.Models;
using BlastGame.Core.Services;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace BlastGame.Core.Views
{
    public class PowerUpView : FieldItemView
    {
        [SerializeField]
        private PowerUpType _tier;

        public PowerUpType Tier => _tier;

        public class Pool : MonoMemoryPool<Vector3, PowerUpView>
        {
            protected override void Reinitialize(Vector3 position, PowerUpView stoneView)
            {
                stoneView.Reset(position);
            }
        }

        public void Initialize(IFieldItemEventListener eventListener)
        {
            _eventListener = eventListener;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            _eventListener.PowerUpClicked(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (LayerIdentifier.IsLayer(collision.gameObject.layer, LayerIdentifier.POWERUP_LAYER))
            {
                var powerUp = collision.gameObject.GetComponent<PowerUpView>();
                _eventListener.PowerUpsTouched(this, powerUp);
            }
        }

        private void Reset(Vector3 position)
        {
            transform.position = position;
        }
    }
}
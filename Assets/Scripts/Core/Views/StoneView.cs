using UnityEngine;
using UnityEngine.EventSystems;
using BlastGame.Core.Services;
using BlastGame.Core.Controllers;
using Zenject;

namespace BlastGame.Core.Views
{
    public class StoneView : FieldItemView
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private IColorProvider _colorProvider;

        private int _id;

        [Inject]
        public void Construct(IColorProvider colorProvider)
        {
            _colorProvider = colorProvider;
        }

        public int Id => _id;

        public class Pool : MonoMemoryPool<Vector3, StoneView>
        {
            protected override void Reinitialize(Vector3 position, StoneView stoneView)
            {
                stoneView.Reset(position);
            }
        }

        private void Reset(Vector3 position)
        {
            transform.position = position;
        }

        public void Initialize(IFieldItemEventListener eventListener, int id)
        {
            _id = id;
            _eventListener = eventListener;
            _spriteRenderer.color = _colorProvider.GetColorById(_id);
        }
        public override void OnPointerDown(PointerEventData eventData)
        {
            _eventListener.StoneClicked(this);
        }

    }
}
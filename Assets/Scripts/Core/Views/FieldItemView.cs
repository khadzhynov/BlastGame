using BlastGame.Core.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BlastGame.Core.Views
{
    public abstract class FieldItemView : MonoBehaviour, IPointerDownHandler
    {
        protected IFieldItemEventListener _eventListener;

        public abstract void OnPointerDown(PointerEventData eventData);
    }
}

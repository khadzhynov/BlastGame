using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GG.Infrastructure.Popups
{
    public class PopupViewBase : MonoBehaviour
    {
        [SerializeField]
        private List<Button> _closeButtons;

        public event Action CloseButtonPressed;

        public event Action OnHidden;
        public event Action OnShown;

        protected virtual void OnEnable()
        {
            foreach (var button in _closeButtons)
            {
                button.onClick.AddListener(OnCloseButton);
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var button in _closeButtons)
            {
                button.onClick.RemoveAllListeners();
            }
        }

        private void OnCloseButton()
        {
            CloseButtonPressed?.Invoke();
        }

        public virtual void Hide()
        {
            Destroy(gameObject);
            OnHiddenEvent();
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
            OnShownEvent();
        }

        protected void OnHiddenEvent()
        {
            OnHidden?.Invoke();
        }

        protected void OnShownEvent()
        {
            OnShown?.Invoke();
        }
    }
}
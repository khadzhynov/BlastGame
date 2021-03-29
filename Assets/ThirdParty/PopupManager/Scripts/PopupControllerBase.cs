using System;

namespace GG.Infrastructure.Popups
{
    public abstract class PopupControllerBase
    {
        public event Action<PopupControllerBase> Closed;

        protected PopupViewBase ViewBase;

        /// <summary>
        /// Short-cut for getting exact PopupViewBase implementation:
        /// return typeof(PopupViewBase);
        /// </summary>
        /// <returns></returns>
        public abstract Type ViewType { get; }

        public bool IsShown
        {
            get => ViewBase != null && ViewBase.enabled == true && ViewBase.gameObject.activeInHierarchy == true;
        }

        public void SetView(PopupViewBase view)
        {
            ViewBase = view;
            OnViewSet();
        }

        protected virtual void OnViewSet()
        {
            ViewBase.CloseButtonPressed += OnCloseButtonPressed;
        }

        private void OnCloseButtonPressed()
        {
            Hide();
        }

        public virtual void Hide()
        {
            Closed?.Invoke(this);
            ViewBase.Hide();
            Dispose();
        }

        public virtual void Show()
        {
            ViewBase.Show();
        }

        protected virtual void Dispose()
        {
            ViewBase.CloseButtonPressed -= OnCloseButtonPressed;
        }
    }
}

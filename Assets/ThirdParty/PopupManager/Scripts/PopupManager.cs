using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

namespace GG.Infrastructure.Popups
{
    public interface IPopupManager
    {
        TController GetPopup<TController, TFactory>()
            where TController : PopupControllerBase
            where TFactory : PlaceholderFactory<TController>;
        event Action OnPopupShown;
        event Action OnPopupHidden;
    }

    public class PopupManager : MonoBehaviour, IPopupManager
    {
        [SerializeField]
        private PopupRegistry _registry;

        public event Action OnPopupShown;
        public event Action OnPopupHidden;

        private List<PopupControllerBase> _activePopups = new List<PopupControllerBase>();

        public TController GetPopup<TController, TFactory>()
            where TController : PopupControllerBase
            where TFactory : PlaceholderFactory<TController>
        {
            TController popupController = _registry.GetFactory<TController, TFactory>().Create();

            int index = _registry.Popups.FindIndex(x => x.GetType() == popupController.ViewType);

            Assert.AreNotEqual(index, -1, "Can not find popup! " + typeof(TController).ToString());

            PopupViewBase popupView = Instantiate(_registry.Popups[index], transform) as PopupViewBase;

            popupController.SetView(popupView);

            popupController.Closed += OnPopupClosed;

            _activePopups.Add(popupController);

            OnPopupShown?.Invoke();

            return popupController;
        }

        private void OnPopupClosed(PopupControllerBase popup)
        {
            popup.Closed -= OnPopupClosed;
            _activePopups.Remove(popup);
            OnPopupHidden?.Invoke();
        }

        public bool IsPopupShown(Type popupType)
        {
            return _activePopups.FindIndex(x => x.GetType() == popupType) != -1;
        }

        public TController GetExistPopup<TController>() where TController : PopupControllerBase
        {
            return _activePopups.Find(x => x.GetType() == typeof(TController)) as TController;
        }

        private void OnDestroy()
        {
            while (_activePopups != null && _activePopups.Count > 0)
            {
                _activePopups[0].Hide();
            }
        }
    }
}

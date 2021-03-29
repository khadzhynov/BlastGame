using BlastGame.UI;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace GG.Infrastructure.Popups
{
    [CreateAssetMenu]
    public class PopupRegistry : ScriptableObjectInstaller
    {
        [SerializeField]
        public List<PopupViewBase> Popups;

        [SerializeField]
        private PopupManager _popupManagerCanvas;

        public TFactory GetFactory<TController, TFactory>()
            where TController : PopupControllerBase
            where TFactory : PlaceholderFactory<TController>
        {
            return Container.Resolve<TFactory>();
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PopupManager>().FromComponentInNewPrefab(_popupManagerCanvas).AsSingle();

            Container.BindFactory<WinPopupController, WinPopupController.Factory>();
            Container.BindFactory<LosePopupController, LosePopupController.Factory>();
        }
    }
}


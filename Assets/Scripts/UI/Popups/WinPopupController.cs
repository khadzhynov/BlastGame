using GG.Infrastructure.Popups;
using System;
using Zenject;


namespace BlastGame.UI
{
    public class WinPopupController : PopupControllerBase
    {
        public class Factory : PlaceholderFactory<WinPopupController> { }
        public override Type ViewType => typeof(WinPopupView);
        protected WinPopupView View => ViewBase as WinPopupView;
    }
}
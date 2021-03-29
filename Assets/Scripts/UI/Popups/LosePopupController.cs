using GG.Infrastructure.Popups;
using System;
using Zenject;


namespace BlastGame.UI
{
    public class LosePopupController : PopupControllerBase
    {
        public class Factory : PlaceholderFactory<LosePopupController> { }
        public override Type ViewType => typeof(LosePopupView);
        protected LosePopupView View => ViewBase as LosePopupView;
    }
}

using GG.Infrastructure.Popups;
using System;
using Zenject;

namespace GG.Infrastructure
{
    public class InGameMenuPopupController : PopupControllerBase
    {
        public enum ResultType
        {
            None = -1,
            Restart = 1,
            //Options = 2,
            TapInput = 3,
            DragInput = 4,
            HoldInput = 5,
            Exit = 6
        }

        public class Factory : PlaceholderFactory<InGameMenuPopupController> { }

        public override Type ViewType => typeof(InGameMenuPopupView);
        protected InGameMenuPopupView View => ViewBase as InGameMenuPopupView;

        private ResultType _result;

        public ResultType Result => _result;

        protected override void OnViewSet()
        {
            _result = ResultType.None;

            base.OnViewSet();

            View.OnHidden += OnHiddenHandler;

            View.OnResult += OnResultHandler;
        }

        private void OnResultHandler(ResultType result)
        {
            _result = result;
            Hide();
        }

        private void OnHiddenHandler()
        {
            View.OnHidden -= OnHiddenHandler;
            View.OnResult -= OnResultHandler;
        }
    }
}
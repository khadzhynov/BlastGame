using System;
using Zenject;

namespace GG.Infrastructure.Popups
{
    public class ChoicePopupController : PopupControllerBase
    {
        public class Factory : PlaceholderFactory<ChoicePopupController> { }
        public override Type ViewType => typeof(ChoicePopupView);
        protected ChoicePopupView View => ViewBase as ChoicePopupView;


        public event Action<ChoicePopupController, bool> Choosed;

        protected override void OnViewSet()
        {
            View.Choosed += OnChoosed;
        }

        private void OnChoosed(bool choice)
        {
            Choosed?.Invoke(this, choice);
            Hide();
        }

        protected override void Dispose()
        {
            base.Dispose();
            View.Choosed -= OnChoosed;
        }

        public void SetText(string message, string header, string buttonTrue, string buttonFalse)
        {
            View.SetMessage(message);
            View.SetHeader(header);
            View.SetButtonsText(buttonTrue, buttonFalse);
        }
    }
}

using System;
using Zenject;

namespace GG.Infrastructure.Popups
{
    public class MessagePopupController : PopupControllerBase
    {
        public class Factory : PlaceholderFactory<MessagePopupController> { }
        public override Type ViewType => typeof(MessagePopupView);
        protected MessagePopupView View => ViewBase as MessagePopupView;

        public void SetText(string message, string header, string button)
        {
            View.SetMessage(message);
            View.SetHeader(header);
            View.SetButtonText(button);
        }
    }
}

using TMPro;
using UnityEngine;

namespace GG.Infrastructure.Popups
{
    public class CustomTextPopupView : PopupViewBase
    {
        [SerializeField]
        private TextMeshProUGUI _message;

        [SerializeField]
        private TextMeshProUGUI _header;

        public void SetMessage(string message)
        {
            _message.text = message;
        }

        public void SetHeader(string header)
        {
            _header.text = header;
        }
    }
}

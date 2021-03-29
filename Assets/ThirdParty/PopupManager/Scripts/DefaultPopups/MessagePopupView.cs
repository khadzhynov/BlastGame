using TMPro;
using UnityEngine;

namespace GG.Infrastructure.Popups
{
    public class MessagePopupView : CustomTextPopupView
    {
        [SerializeField]
        private TextMeshProUGUI _buttonText;

        public void SetButtonText(string text)
        {
            if (_buttonText != null)
            {
                _buttonText.text = text;
            }
        }
    }
}

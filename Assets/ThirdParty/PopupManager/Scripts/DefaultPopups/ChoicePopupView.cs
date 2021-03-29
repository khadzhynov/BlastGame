using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GG.Infrastructure.Popups
{
    public class ChoicePopupView : CustomTextPopupView
    {
        [SerializeField]
        private Button _true;

        [SerializeField]
        private Button _false;

        [SerializeField]
        private TextMeshProUGUI _trueText;
        [SerializeField]
        private TextMeshProUGUI _falseText;

        public Action<bool> Choosed;

        protected override void OnEnable()
        {
            base.OnEnable();
            _true.onClick.AddListener(OnTruePressed);
            _false.onClick.AddListener(OnFalsePressed);
        }

        private void OnTruePressed()
        {
            Choosed?.Invoke(true);
        }

        private void OnFalsePressed()
        {
            Choosed?.Invoke(false);
        }

        public void SetButtonsText(string trueText, string falseText)
        {
            if (_trueText != null)
            {
                _trueText.text = trueText;
            }

            if (_falseText != null)
            {
                _falseText.text = falseText;
            }
        }
    }
}

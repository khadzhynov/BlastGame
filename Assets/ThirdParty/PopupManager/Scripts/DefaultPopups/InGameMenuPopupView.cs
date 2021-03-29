using GG.Infrastructure.Popups;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GG.Infrastructure
{

    public class InGameMenuPopupView : PopupViewFadable
    {
        public event Action<InGameMenuPopupController.ResultType> OnResult;

        [SerializeField]
        private Button _buttonPrefab;

        [SerializeField]
        private Transform _buttonsContainer;

        private void Start()
        {
            foreach (int item in Enum.GetValues(typeof(InGameMenuPopupController.ResultType)))
            {
                if (item >= 0)
                {
                    var itemName = ((InGameMenuPopupController.ResultType)item).ToString();
                    var newButton = Instantiate(_buttonPrefab, _buttonsContainer);
                    newButton.gameObject.SetActive(true);
                    newButton.GetComponentInChildren<TextMeshProUGUI>().text = itemName;
                    newButton.onClick.AddListener(() => OnClickListener(itemName));
                }
            }
        }

        private void OnClickListener(string resultString)
        {
            InGameMenuPopupController.ResultType result;
            if (Enum.TryParse(resultString, out result))
            {
                OnResult?.Invoke(result);
            }
        }
    }
}

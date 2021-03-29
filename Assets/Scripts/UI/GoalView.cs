using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BlastGame.Core.Models;

namespace BlastGame.UI
{
    public class GoalView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Image _reached;

        [SerializeField]
        private TextMeshProUGUI _amount;

        private Goal _subscribedGoal;

        private void OnDestroy()
        {
            _subscribedGoal.OnChanged -= OnChangedHandler;
        }

        public void SubscribeGoalChange(Goal goal)
        {
            _subscribedGoal = goal;
            goal.OnChanged += OnChangedHandler;
        }

        private void OnChangedHandler(Goal goal)
        {
            int amountLeft = goal.Target - goal.Amount;
            if (amountLeft > 0)
            {
                _amount.gameObject.SetActive(true);
                _reached.gameObject.SetActive(false);
                _amount.text = amountLeft.ToString();
            }
            else
            {
                _amount.gameObject.SetActive(false);
                _reached.gameObject.SetActive(true);
                _subscribedGoal.OnChanged -= OnChangedHandler;
            }
        }

        public void SetColor(Color color)
        {
            _icon.color = color;
        }

        public void SetIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }

        public void SetAmount(int amount)
        {
            _amount.text = amount.ToString();
        }
    }
}
using UnityEngine;
using TMPro;
using Zenject;
using BlastGame.Core;
using BlastGame.Core.Models;

namespace BlastGame.UI
{
    public class MovesWidget : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _amount;

        private LevelProgressModel _levelProgressModel;

        [Inject]
        public void Construct(LevelProgressModel levelProgressModel)
        {
            _levelProgressModel = levelProgressModel;
        }

        private void OnEnable()
        {
            _levelProgressModel.OnMovesChanged += OnMovesChangedHandler;
        }

        private void OnDisable()
        {
            _levelProgressModel.OnMovesChanged -= OnMovesChangedHandler;
        }

        private void OnMovesChangedHandler(int moves)
        {
            _amount.text = moves.ToString();
        }
    }
}
using BlastGame.Core.Models;
using BlastGame.Core.Services;
using UnityEngine;
using Zenject;

namespace BlastGame.UI
{
    public class GoalsWidget : MonoBehaviour
    {
        [SerializeField]
        private GoalView _goalPrefab;

        private IconsModel _iconsModel;
        private LevelProgressModel _levelProgressModel;
        private GameplayModel _gameplayModel;

        [Inject]
        public void Construct(IconsModel iconsModel, LevelProgressModel levelProgressModel, GameplayModel gameplayModel)
        {
            _iconsModel = iconsModel;
            _levelProgressModel = levelProgressModel;
            _gameplayModel = gameplayModel;
        }

        private void Start()
        {
            IColorProvider colorProvider = new HSVColorProvider(_gameplayModel.MaxColors);

            foreach (var goal in _levelProgressModel.Goals)
            {
                var newGoalView = Instantiate(_goalPrefab, transform);
                newGoalView.gameObject.SetActive(true);
                newGoalView.SetIcon(_iconsModel.GetGoalIcon(goal.GoalType, goal.Id));

                if (goal.GoalType == GoalType.Stone)
                {
                    newGoalView.SetColor(colorProvider.GetColorById(goal.Id));
                }

                newGoalView.SetAmount(goal.Target - goal.Amount);
                newGoalView.SubscribeGoalChange(goal);
            }
        }
    }
}
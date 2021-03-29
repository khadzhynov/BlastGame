using BlastGame.Core.Models;
using BlastGame.Core.Views;
using System;

namespace BlastGame.Core.Controllers
{
    public class LevelProgressController : IDisposable
    {
        public event Action OnWin;
        public event Action OnLose;

        private LevelProgressModel _levelProgressModel;
        private ExplosionController _explosionController;
        private GameplayController _gameplayController;

        public LevelProgressController(
            LevelProgressModel levelProgressModel,
            ExplosionController explosionController)
        {
            _levelProgressModel = levelProgressModel;
            _explosionController = explosionController;

            _explosionController.OnFieldItemExploded += OnFieldItemExplodedHandler;

            _levelProgressModel.Moves = _levelProgressModel.InitialMoves;
            foreach (var goal in _levelProgressModel.Goals)
            {
                goal.Reset();
            }
        }

        public void AssignGameplayController(GameplayController gameplayController)
        {
            _gameplayController = gameplayController;
            _gameplayController.OnStoneCollapsed += OnStoneCollapsedHandler;
            _gameplayController.OnPowerUpChainReactionDoneAfterClick += OnPowerUpChainReactionDoneAfterClickHandler;
            _gameplayController.OnStoneCollapseTriggered += OnStoneCollapseTriggeredHandler;
        }

        private void OnStoneCollapsedHandler(StoneView stone)
        {
            CheckStoneGoal(stone);
        }

        private void OnStoneCollapseTriggeredHandler()
        {
            UpdateMoves();
        }

        private void OnFieldItemExplodedHandler(FieldItemView item)
        {
            if (item is PowerUpView powerUp)
            {
                CheckPowerUpGoal(powerUp);
            }

            if (item is StoneView stone)
            {
                CheckStoneGoal(stone);
            }
        }

        private void OnPowerUpChainReactionDoneAfterClickHandler()
        {
            UpdateMoves();
        }

        private void UpdateMoves()
        {
            _levelProgressModel.Moves--;
            if (_levelProgressModel.Moves <= 0)
            {
                OnLose?.Invoke();
            }
        }

        private void CheckStoneGoal(StoneView stone)
        {
            var goal = _levelProgressModel.Goals.Find(x => x.GoalType == GoalType.Stone && x.Id == stone.Id);
            if (goal != null)
            {
                IncrementGoal(goal);
            }
        }

        private void CheckPowerUpGoal(PowerUpView powerUp)
        {
            var goal = _levelProgressModel.Goals.Find(x => x.GoalType == GoalType.PowerUp && x.Id == (int)powerUp.Tier);
            if (goal != null)
            {
                IncrementGoal(goal);
            }
        }

        private void IncrementGoal(Goal goal)
        {
            goal.Increment();
            CheckAllGoals();
        }

        private void CheckAllGoals()
        {
            if (_levelProgressModel.HasWin == false)
            {
                if (_levelProgressModel.Goals.Find(x => !x.IsReached()) == null)
                {
                    _levelProgressModel.HasWin = true;
                    OnWin?.Invoke();
                }
            }
        }

        public void Dispose()
        {
            _explosionController.OnFieldItemExploded -= OnFieldItemExplodedHandler;
            _gameplayController.OnStoneCollapsed -= OnStoneCollapsedHandler;
            _gameplayController.OnPowerUpChainReactionDoneAfterClick -= OnPowerUpChainReactionDoneAfterClickHandler;
            _gameplayController.OnStoneCollapseTriggered -= OnStoneCollapseTriggeredHandler;
        }
    }
}
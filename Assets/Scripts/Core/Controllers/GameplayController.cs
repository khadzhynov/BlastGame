using BlastGame.Core.Models;
using BlastGame.Core.Pools;
using BlastGame.Core.Views;
using BlastGame.UI;
using GG.Infrastructure.Popups;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlastGame.Core.Controllers
{
    public class GameplayController : IFieldItemEventListener, IDisposable
    {
        public event Action<StoneView> OnStoneCollapsed;
        public event Action OnPowerUpChainReactionDoneAfterClick;
        public event Action OnStoneCollapseTriggered;

        private GameplayModel _gameplayModel;
        private StonesController _stonesController;
        private SpawnController _spawnController;
        private PowerUpsModel _powerUpsModel;
        private ExplosionController _explosionController;
        private PopupManager _popupManager;
        private LevelProgressController _levelProgressController;

        public GameplayController(
            GameplayModel gameplayModel,
            StonesController stonesController,
            SpawnController spawnController,
            PowerUpsModel powerUpsModel,
            ExplosionController explosionController,
            PopupManager popupManager,
            LevelProgressController levelProgressController)
        {
            _gameplayModel = gameplayModel;
            _stonesController = stonesController;
            _spawnController = spawnController;
            _spawnController.AssignEventListener(this);
            _powerUpsModel = powerUpsModel;
            _explosionController = explosionController;
            _popupManager = popupManager;
            _levelProgressController = levelProgressController;
            _levelProgressController.AssignGameplayController(this);
            
            _explosionController.OnVictimsDestroyed += OnVictimsDestroyedHandler;
            _levelProgressController.OnWin += OnWinHandler;
            _levelProgressController.OnLose += OnLoseHandler;

            _spawnController.SpawnStone(_gameplayModel.InitialStonesAmount);

            _gameplayModel.IsInputLocked = false;
            
        }

        public void StoneClicked(StoneView stone)
        {
            if (!_gameplayModel.IsInputLocked)
            {
                List<StoneView> nearWithId = _stonesController.GetNearStonesSameId(stone);

                if (nearWithId.Count >= _gameplayModel.MinNumberToCollapse)
                {
                    SpawnPowerUp(nearWithId.Count, stone.transform.position);

                    CollapseStones(nearWithId);

                    _spawnController.SpawnStone(nearWithId.Count);

                    OnStoneCollapseTriggered?.Invoke();
                }
            }
        }

        public void PowerUpClicked(PowerUpView powerUp)
        {
            if (!_gameplayModel.IsInputLocked)
            {
                _gameplayModel.IsInputLocked = true;
                _explosionController.ExplodePowerUp(
                    powerUp,
                    () =>
                    {
                        OnPowerUpChainReactionDoneAfterClick?.Invoke();
                        _gameplayModel.IsInputLocked = false;
                    });
            }
        }

        public void PowerUpsTouched(PowerUpView first, PowerUpView second)
        {
            if (_powerUpsModel.PowerUps.Contains(first) && _powerUpsModel.PowerUps.Contains(second))
            {
                if (first.Tier == second.Tier && first.Tier != PowerUpType.Round)
                {
                    MergePowerUps(first, second);
                }
            }
        }

        private void MergePowerUps(PowerUpView first, PowerUpView second)
        {
            var position = (first.transform.position + second.transform.position) / 2;
            var tier = (PowerUpType)((int)first.Tier + 1);
            var newPowerUp = _spawnController.SpawnPowerUp(tier, position);
            _spawnController.RemovePowerUp(first);
            _spawnController.RemovePowerUp(second);
        }

        private void CollapseStones(List<StoneView> stones)
        {
            foreach (var stone in stones)
            {
                _spawnController.RemoveStone(stone);
                OnStoneCollapsed?.Invoke(stone);
            }
        }

        private void SpawnPowerUp(int collapsed, Vector3 position)
        {
            if (collapsed >= _powerUpsModel.AmountToPowerUp)
            {
                var newPowerUp = _spawnController.SpawnPowerUp(PowerUpType.Line, position);
            }
        }

        private void OnLoseHandler()
        {
            _gameplayModel.IsInputLocked = true;
            var losePopup = _popupManager.GetPopup<LosePopupController, LosePopupController.Factory>();
            losePopup.Closed += GameEndPopupClosedHandler;
            losePopup.Show();
        }

        private void OnWinHandler()
        {
            _gameplayModel.IsInputLocked = true;
            var winPopup = _popupManager.GetPopup<WinPopupController, WinPopupController.Factory>();
            winPopup.Closed += GameEndPopupClosedHandler;
            winPopup.Show();
        }

        private void GameEndPopupClosedHandler(PopupControllerBase popupController)
        {
            popupController.Closed -= GameEndPopupClosedHandler;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnVictimsDestroyedHandler(int amount)
        {
            _spawnController.SpawnStone(amount);
        }

        public void Dispose()
        {
            _explosionController.OnVictimsDestroyed -= OnVictimsDestroyedHandler;
            _levelProgressController.OnWin -= OnWinHandler;
            _levelProgressController.OnLose -= OnLoseHandler;
        }
    }
}
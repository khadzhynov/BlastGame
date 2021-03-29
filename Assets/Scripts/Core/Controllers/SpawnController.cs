using BlastGame.Core.Models;
using BlastGame.Core.Pools;
using BlastGame.Core.Views;
using BlastGame.Services;
using System.Collections;
using UnityEngine;
using Zenject;

namespace BlastGame.Core.Controllers
{

    public class SpawnController : MonoBehaviour
    {
        [SerializeField]
        private Transform _spawnPoint;
        private SpawnModel _spawnModel;
        private StonesController _stonesController;
        private IFieldItemEventListener _fieldItemEventListener;
        private CoroutineService _coroutineService;
        private GameplayModel _gameplayModel;
        private StonesPool _stonesPool;
        private PowerUpsPool _powerUpsPool;
        private PowerUpsModel _powerUpsModel;

        [Inject]
        private void Construct(
            SpawnModel spawnModel,
            StonesController stonesController,
            CoroutineService coroutineService,
            GameplayModel gameplayModel,
            StonesPool stonesPool,
            PowerUpsPool powerUpsPool,
            PowerUpsModel powerUpsModel)
        {
            _spawnModel = spawnModel;
            _stonesController = stonesController;
            _coroutineService = coroutineService;
            _gameplayModel = gameplayModel;
            _stonesPool = stonesPool;
            _powerUpsPool = powerUpsPool;
            _powerUpsModel = powerUpsModel;
        }

        public void AssignEventListener(IFieldItemEventListener fieldItemEventListener)
        {
            _fieldItemEventListener = fieldItemEventListener;
        }

        public PowerUpView SpawnPowerUp(PowerUpType tier, Vector3 position)
        {
            var powerUp = _powerUpsPool.Add(position, tier);
            _powerUpsModel.PowerUps.Add(powerUp);
            powerUp.Initialize(_fieldItemEventListener);
            return powerUp;
        }

        public void RemovePowerUp(PowerUpView powerUp)
        {
            _powerUpsModel.PowerUps.Remove(powerUp);
            _powerUpsPool.Remove(powerUp);
        }

        public void SpawnStone(int amount)
        {

            if (_spawnModel.SpawnQueue.Count == 0)
            {
                _spawnModel.SpawnQueue.Enqueue(amount);
                StartCoroutine(SpawnRoutine());
            }
            else
            {
                _spawnModel.SpawnQueue.Enqueue(amount);
            }
        }

        private IEnumerator SpawnRoutine()
        {
            while (_spawnModel.SpawnQueue.Count > 0)
            {
                int amount = _spawnModel.SpawnQueue.Dequeue();

                float spawnDistance = Mathf.Lerp(0, _spawnModel.SpawnMaxDistance, Mathf.Clamp01(amount / _spawnModel.SpawnAmountForMaxDistance));

                for (int i = 0; i < amount; ++i)
                {
                    SpawnSingleStone(spawnDistance);
                    yield return _coroutineService.WaitTime(_spawnModel.SpawnDelay);
                }
            }
        }

        private void SpawnSingleStone(float maxDistance)
        {
            Vector3 position = _spawnPoint.position + (Vector3)Random.insideUnitCircle * maxDistance;
            var stone = _stonesPool.Add(position);
            int id = Random.Range(0, _gameplayModel.MaxColors);
            stone.Initialize(_fieldItemEventListener, id);
            _stonesController.AddStone(stone);
        }

        public void RemoveStone(StoneView stone)
        {
            _stonesController.RemoveStone(stone);
            _stonesPool.Remove(stone);
        }
    }
}
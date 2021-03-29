using BlastGame.Core.Controllers;
using BlastGame.Core.Models;
using BlastGame.Core.Pools;
using BlastGame.Core.Services;
using BlastGame.Core.Views;
using System;
using UnityEngine;
using Zenject;

namespace BlastGame.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private GameplayModel _gameplayModel;

        [SerializeField]
        private SpawnModel _spawnModel;

        [SerializeField]
        private PowerUpsModel _powerUpsModel;

        [SerializeField]
        private LevelProgressModel _levelProgressModel;

        [SerializeField]
        private SpawnController _spawnController;

        [SerializeField]
        public GameObject _stonePrefab;

        public override void InstallBindings()
        {
            BindModels();
            BindControllers();
            BindServices();
            BindPools();
        }

        private void BindModels()
        {
            Container.BindInterfacesAndSelfTo<GameplayModel>().FromInstance(_gameplayModel).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<SpawnModel>().FromInstance(_spawnModel).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PowerUpsModel>().FromInstance(_powerUpsModel).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelProgressModel>().FromInstance(_levelProgressModel).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<StonesModel>().FromNew().AsSingle().NonLazy();
        }

        private void BindControllers()
        {
            Container.BindInterfacesAndSelfTo<GameplayController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ExplosionController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<StonesController>().FromNew().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<LevelProgressController>().FromNew().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<SpawnController>().FromInstance(_spawnController).AsSingle().NonLazy();
        }

        private void BindServices()
        {
            Container.BindInterfacesAndSelfTo<HSVColorProvider>().FromInstance(new HSVColorProvider(_gameplayModel.MaxColors)).AsSingle().NonLazy();
        }

        private void BindPools()
        {
            Container.Bind<StonesPool>().AsSingle().NonLazy();
            Container.BindMemoryPool<StoneView, StoneView.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(_spawnModel.StonePrefab)
                .UnderTransformGroup(nameof(StoneView));

            foreach (int powerUpTypeIndex in Enum.GetValues(typeof(PowerUpType)))
            {
                if (powerUpTypeIndex != 0)
                {
                    var prefab = _spawnModel.GetPowerUpPrefab((PowerUpType)powerUpTypeIndex);
                    Container.BindMemoryPool<PowerUpView, PowerUpView.Pool>()
                        .WithId((PowerUpType)powerUpTypeIndex)
                        .WithInitialSize(10)
                        .FromComponentInNewPrefab(prefab)
                        .UnderTransformGroup(nameof(PowerUpView));
                }
            }

            Container.Bind<PowerUpsTier1Pool>().AsSingle().NonLazy();
            Container.Bind<PowerUpsTier2Pool>().AsSingle().NonLazy();
            Container.Bind<PowerUpsTier3Pool>().AsSingle().NonLazy();
            Container.Bind<PowerUpsPool>().AsSingle().NonLazy();
            
        }
    }
}
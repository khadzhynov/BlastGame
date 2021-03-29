using BlastGame.Core.Models;
using BlastGame.Core.Views;
using BlastGame.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace BlastGame.Core.Controllers
{
    public class ExplosionController
    {
        public event Action<int> OnVictimsDestroyed;
        public event Action<FieldItemView> OnFieldItemExploded;

        private PowerUpsModel _powerUpsModel;
        private StonesController _stonesController;
        private CoroutineService _coroutineService;
        private SpawnController _spawnController;

        public ExplosionController(
            PowerUpsModel powerUpsModel,
            StonesController stonesController,
            CoroutineService coroutineService,
            SpawnController spawnController)
        {
            _powerUpsModel = powerUpsModel;
            _stonesController = stonesController;
            _coroutineService = coroutineService;
            _spawnController = spawnController;
        }

        public void ExplodePowerUp(PowerUpView powerUp, Action callback)
        {
            _coroutineService.StartCoroutine(ExplodeRoutine(powerUp, callback));
        }

        private IEnumerator ExplodeRoutine(PowerUpView powerUp, Action callback)
        {
            var chainReaction = new Queue<PowerUpView>();
            chainReaction.Enqueue(powerUp);
            _powerUpsModel.PowerUps.Remove(powerUp);

            while (chainReaction.Count > 0)
            {
                yield return ExplosionRound(chainReaction);
            }

            callback?.Invoke();
        }

        private IEnumerator ExplosionRound(Queue<PowerUpView> chainReaction)
        {
            var explosive = chainReaction.Dequeue();

            if (explosive != null)
            {
                List<FieldItemView> victims = Explode(explosive);

                foreach (var victim in victims)
                {
                    ProcessExplosionVictim(chainReaction, victim);
                }

                yield return _coroutineService.WaitTime(_powerUpsModel.ChainReactionDelay);

                if (victims.Count > 0)
                {
                    OnVictimsDestroyed?.Invoke(victims.Count);
                }
            }
        }

        private List<FieldItemView> Explode(PowerUpView explosive)
        {
            var explosion = _powerUpsModel.GetExplosion(explosive.Tier);

            List<FieldItemView> victims = GetExplosionVictims(explosion, explosive.transform.position);

            var effect = SimplePool.Spawn<ParticleSystem>(explosion.Effect, explosive.transform.position, Quaternion.identity);

            _coroutineService.DelayedDespawn(effect.main.duration, effect.gameObject);

            OnFieldItemExploded?.Invoke(explosive);

            _spawnController.RemovePowerUp(explosive);
            return victims;
        }

        private List<FieldItemView> GetExplosionVictims(ExplosionModel explosion, Vector3 origin)
        {
            Assert.IsNotNull(explosion);

            List<FieldItemView> fieldItems = _stonesController.GetAllStones().Cast<FieldItemView>().ToList();
            fieldItems.AddRange(_powerUpsModel.PowerUps.Cast<FieldItemView>().ToList());

            var victims = explosion.GetVictims(fieldItems, origin);
            return victims;
        }

        private void ProcessExplosionVictim(Queue<PowerUpView> chainReaction, FieldItemView victim)
        {
            if (victim is PowerUpView victimPowerUp)
            {
                _powerUpsModel.PowerUps.Remove(victimPowerUp);
                chainReaction.Enqueue(victimPowerUp);
            }

            if (victim is StoneView victimStone)
            {
                OnFieldItemExploded?.Invoke(victim);
                _spawnController.RemoveStone(victimStone);
            }
        }
    }
}
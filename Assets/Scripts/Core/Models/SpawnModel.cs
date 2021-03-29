using BlastGame.Core.Views;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.Core.Models
{
    [CreateAssetMenu]
    public class SpawnModel : ScriptableObject
    {
        [SerializeField]
        private float _spawnDelay = 0.25f;

        [SerializeField]
        private float _spawnMaxDistance = 0.5f;

        [SerializeField]
        private float _spawnAmountForMaxDistance = 30;

        [SerializeField]
        private StoneView _stonePrefab;

        [SerializeField]
        private List<PowerUpView> _powerUpPrefabs;

        public float SpawnDelay => _spawnDelay;

        public StoneView StonePrefab => _stonePrefab;

        public float SpawnMaxDistance => _spawnMaxDistance;

        public float SpawnAmountForMaxDistance => _spawnAmountForMaxDistance;

        public Queue<int> SpawnQueue { get; } = new Queue<int>();

        public PowerUpView GetPowerUpPrefab(PowerUpType tier)
        {
            return _powerUpPrefabs[(int)tier - 1];
        }
    }
}
using BlastGame.Core.Views;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.Core.Models
{
    [CreateAssetMenu]
    public class PowerUpsModel : ScriptableObject
    {
        [SerializeField]
        private List<ExplosionModel> _explosions;

        [SerializeField]
        private int _amountToPowerUp;

        [SerializeField]
        private float _chainReactionDelay = 1f;

        public int AmountToPowerUp => _amountToPowerUp;
        public float ChainReactionDelay => _chainReactionDelay;
        public List<PowerUpView> PowerUps { get; } = new List<PowerUpView>();

        public ExplosionModel GetExplosion(PowerUpType tier)
        {
            //TODO: replace with serializable dictionary
            return _explosions.Find(x => x.Tier == tier);
        }
    }
}
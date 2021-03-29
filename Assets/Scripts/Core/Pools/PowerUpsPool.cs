using BlastGame.Core.Models;
using BlastGame.Core.Views;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlastGame.Core.Pools
{
    public class PowerUpsPool
    {
        private Dictionary<PowerUpType, PowerUpPoolBase> _pools;

        public PowerUpsPool(PowerUpsTier1Pool tier1Pool, PowerUpsTier2Pool tier2Pool, PowerUpsTier3Pool tier3Pool)
        {
            _pools = new Dictionary<PowerUpType, PowerUpPoolBase>();
            _pools.Add(tier1Pool.PowerUpType, tier1Pool);
            _pools.Add(tier2Pool.PowerUpType, tier2Pool);
            _pools.Add(tier3Pool.PowerUpType, tier3Pool);
        }

        public PowerUpView Add(Vector3 position, PowerUpType tier)
        {
            var newItem = _pools[tier].PowerUpsPool.Spawn(position);
            _pools[tier].PowerUps.Add(newItem);
            return newItem;
        }

        public void Remove(PowerUpView powerUp)
        {
            if (_pools[powerUp.Tier].PowerUps.Contains(powerUp))
            {
                _pools[powerUp.Tier].PowerUpsPool.Despawn(powerUp);
                _pools[powerUp.Tier].PowerUps.Remove(powerUp);
            }
        }
    }

    public abstract class PowerUpPoolBase
    {
        protected PowerUpView.Pool _powerUpsPool;

        public PowerUpView.Pool PowerUpsPool => _powerUpsPool;
        public virtual List<PowerUpView> PowerUps => null;
        public virtual PowerUpType PowerUpType => PowerUpType.None;
    }

    public class PowerUpsTier1Pool : PowerUpPoolBase
    {
        protected List<PowerUpView> _powerUps = new List<PowerUpView>();
        public override List<PowerUpView> PowerUps => _powerUps;
        public override PowerUpType PowerUpType => PowerUpType.Line;
        public PowerUpsTier1Pool([Inject(Id = PowerUpType.Line)] PowerUpView.Pool powerUpsPool)
        {
            _powerUpsPool = powerUpsPool;
        }
    }

    public class PowerUpsTier2Pool : PowerUpPoolBase
    {
        protected List<PowerUpView> _powerUps = new List<PowerUpView>();
        public override List<PowerUpView> PowerUps => _powerUps;
        public override PowerUpType PowerUpType => PowerUpType.Cross;
        public PowerUpsTier2Pool([Inject(Id = PowerUpType.Cross)] PowerUpView.Pool powerUpsPool)
        {
            _powerUpsPool = powerUpsPool;
        }
    }

    public class PowerUpsTier3Pool : PowerUpPoolBase
    {
        protected List<PowerUpView> _powerUps = new List<PowerUpView>();
        public override List<PowerUpView> PowerUps => _powerUps;
        public override PowerUpType PowerUpType => PowerUpType.Round;
        public PowerUpsTier3Pool([Inject(Id = PowerUpType.Round)] PowerUpView.Pool powerUpsPool)
        {
            _powerUpsPool = powerUpsPool;
        }
    }
}
using BlastGame.Core.Views;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.Core.Pools
{
    public class StonesPool
    {
        private StoneView.Pool _stonesPool;
        private List<StoneView> _stones = new List<StoneView>();

        public StonesPool(StoneView.Pool stonesPool)
        {
            _stonesPool = stonesPool;
        }

        public StoneView Add(Vector3 position)
        {
            var newItem = _stonesPool.Spawn(position);
            _stones.Add(newItem);
            return newItem;
        }

        public void Remove(StoneView stone)
        {
            if (_stones.Contains(stone))
            {
                _stonesPool.Despawn(stone);
                _stones.Remove(stone);
            }
        }
    }
}
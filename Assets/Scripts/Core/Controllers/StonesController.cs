using BlastGame.Core.Models;
using BlastGame.Core.Views;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlastGame.Core.Controllers
{
    public class StonesController
    {
        private StonesModel _stonesModel;

        public StonesController(StonesModel stonesModel)
        {
            _stonesModel = stonesModel;
        }

        public List<StoneView> GetNearStonesSameId(StoneView stone)
        {
            var nearWithId = new List<StoneView>();

            var stonesWithId = _stonesModel.GetStonesWithId(stone.Id);

            var newStones = new List<StoneView> { stone };

            var stonesToTest = new List<StoneView>();

            float distance = 0.65f;

            int iterations = 1000;

            while (newStones.Count > 0 && iterations > 0)
            {
                iterations--;
                nearWithId.AddRange(newStones);
                stonesToTest.AddRange(newStones);
                newStones.Clear();
                foreach (var testStone in stonesToTest)
                {
                    foreach (var anyStone in stonesWithId)
                    {
                        if (Vector2.Distance(testStone.transform.position, anyStone.transform.position) <= distance)
                        {
                            newStones.Add(anyStone);
                        }
                    }
                }

                newStones = newStones.Except(nearWithId).ToList();
            }

            return nearWithId;
        }

        public void AddStone(StoneView stone)
        {
            _stonesModel.Stones.Add(stone);
        }

        public void RemoveStone(StoneView stone)
        {
            _stonesModel.Stones.Remove(stone);
        }

        public List<StoneView> GetAllStones()
        {
            return _stonesModel.Stones;
        }
    }
}
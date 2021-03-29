using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using BlastGame.Core.Views;

namespace BlastGame.Core.Models
{
    [CreateAssetMenu]
    public class ExplosionModel : ScriptableObject
    {
        [Serializable]
        private class ExplosionRect
        {
            [SerializeField]
            private float _height;

            [SerializeField]
            private float _width;

            public bool IsInside(Vector3 point, Vector3 origin)
            {
                return Mathf.Abs(point.x - origin.x) < _width &&
                    Mathf.Abs(point.y - origin.y) < _height;
            }
        }

        [SerializeField]
        private List<ExplosionRect> _rects;

        [SerializeField]
        private float _radius = 0;

        [SerializeField]
        private ParticleSystem _effect;

        [SerializeField]
        private PowerUpType _tier;

        public PowerUpType Tier => _tier;

        public ParticleSystem Effect => _effect;

        public List<FieldItemView> GetVictims(List<FieldItemView> items, Vector3 position)
        {
            var victims = new List<FieldItemView>();
            if (_radius > 0)
            {
                var radialVictims = items.Where(x => Vector3.Distance(x.transform.position, position) <= _radius);
                if (radialVictims != null)
                {
                    victims.AddRange(radialVictims);
                }
            }

            foreach (var rect in _rects)
            {
                var rectVictims = items.Where(x => rect.IsInside(x.transform.position, position));
                if (rectVictims != null)
                {
                    victims.AddRange(rectVictims);
                }
            }

            return victims;
        }

    }
}
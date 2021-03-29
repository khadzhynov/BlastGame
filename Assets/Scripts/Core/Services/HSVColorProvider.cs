using UnityEngine;

namespace BlastGame.Core.Services
{
    public class HSVColorProvider : IColorProvider
    {
        private int _maxId;

        public HSVColorProvider(int maxId)
        {
            _maxId = maxId;
        }

        public Color GetColorById(int id)
        {
            return Color.HSVToRGB(1f / _maxId * id, 1f, 1f);
        }
    }
}
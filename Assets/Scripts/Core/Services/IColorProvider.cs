using UnityEngine;

namespace BlastGame.Core.Services
{
    public interface IColorProvider
    {
        Color GetColorById(int id);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlastGame.Core.Services
{
    public class LayerIdentifier
    {
        public const string STONE_LAYER = "Stone";
        public const string POWERUP_LAYER = "PowerUp";

        public static bool IsLayer(int layer, string layerName)
        {
            return layer == LayerMask.NameToLayer(layerName);
        }
    }
}

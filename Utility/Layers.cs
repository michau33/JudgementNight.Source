using UnityEngine;

namespace Assets.Scripts.Utility
{
    public static class Layers
    {
        public static readonly int Player = 8;
        public static readonly int Enemy = 9;
        public static readonly int Obstacles = 10;

        /// <summary>
        /// Extension method to check whethher layer is in layermask
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask.value == layer;
        }
        
    }
}
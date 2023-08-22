using System;
using UnityEngine;


namespace Game
{
    [Serializable]
    internal sealed class ShootingConfig
    {
        public int Damage = 1;
        public Vector2 ShootInterval = new(1, 5);
    }
}

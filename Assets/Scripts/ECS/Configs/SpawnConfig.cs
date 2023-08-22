using System;
using UnityEngine;


namespace Game
{
    [Serializable]
    internal sealed class SpawnConfig
    {
        public Transform BlueTeamSpawnPosition;
        public Transform RedTeamSpawnPosition;
        public float DistanceBetweenSpawn = 2;

        public int SpawnCount = 30;
        public int SpawnCountInLine = 10;
    }
}

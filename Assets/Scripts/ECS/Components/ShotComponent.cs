using UnityEngine;


namespace Game.ECS
{
    internal struct ShotComponent
    {
        public Vector3 ShotPositionStart;
        public Vector3 ShotPositionEnd;
        public Team Team;
    }
}

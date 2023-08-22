using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS
{
    internal readonly struct WayPointSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<WayPointComponent, MoveInPointComponent>> _filterWaypoints;
        private readonly EcsCustomInject<SpawnConfig> _config;

        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            var poolWayPoints = _filterWaypoints.Pools.Inc1;
            var poolMoving = _filterWaypoints.Pools.Inc2;

            var poolSpawnReq = systems.GetWorld().GetPool<SpawnRequireComponent>();

            var index = 0;
            foreach (var entity in _filterWaypoints.Value)
            {
                ref var wc = ref poolWayPoints.Get(entity);
                wc.StartPos = GetPosition(index);
                var endIndex = index > _config.Value.SpawnCount - 1 ?
                    index - _config.Value.SpawnCount : index + _config.Value.SpawnCount;
                wc.EndPos = GetPosition(endIndex);
                ref var mc = ref poolMoving.Get(entity);
                mc.TargetPoint = wc.StartPos;

                poolSpawnReq.Get(entity).SpawnPos = wc.StartPos;

                index++;
            }
        }

        private Vector3 GetPosition(int index)
        {
            var teamIndex =
                index < _config.Value.SpawnCount ?
                index :
                index - _config.Value.SpawnCount;


            var x = teamIndex % _config.Value.SpawnCountInLine * _config.Value.DistanceBetweenSpawn;
            var y = 0;
            var z = teamIndex / (float)_config.Value.SpawnCountInLine * _config.Value.DistanceBetweenSpawn;

            var offset = new Vector3(x, y, z);

            if (index >= _config.Value.SpawnCount)
            {
                return _config.Value.RedTeamSpawnPosition.position + offset;
            }

            else
            {
                return _config.Value.BlueTeamSpawnPosition.position + offset;
            }
        }
    }
}

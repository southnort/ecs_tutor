using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS
{
    internal readonly struct BlockInitializeSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<SpawnConfig> _spawnConfig;
        private readonly EcsCustomInject<HealthConfig> _healthConfig;
        private readonly EcsCustomInject<PrefabsConfig> _prefabsConfig;
        private readonly EcsCustomInject<MoveConfig> _moveConfig;


        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var poolWayPoints = world.GetPool<WayPointComponent>();
            var poolMoving = world.GetPool<MoveInPointComponent>();
            var poolHealth = world.GetPool<HealthComponent>();
            var poolViews = world.GetPool<GameObjectComponent>();
            var poolColors = world.GetPool<ColorComponent>();
            var poolTeams = world.GetPool<TeamComponent>();
            var poolShoot = world.GetPool<ShootCooldownComponent>();
            var poolSpawnReq = world.GetPool<SpawnRequireComponent>();


            for (var i = 0; i < _spawnConfig.Value.SpawnCount * 2; i++)
            {
                var entity = world.NewEntity();
                poolWayPoints.Add(entity);
                poolMoving.Add(entity).Speed = _moveConfig.Value.BlockSpeed;
                poolHealth.Add(entity).Health = _healthConfig.Value.Health;
                poolViews.Add(entity);
                poolShoot.Add(entity);

                ref var spawnReqC = ref poolSpawnReq.Add(entity);
                spawnReqC.PrefabPath = _prefabsConfig.Value.BlockPrefabPath;
                spawnReqC.SpawnPos = Vector3.zero;

                ref var colorC = ref poolColors.Add(entity);
                colorC.TempoColor = Color.yellow;

                if (i >= _spawnConfig.Value.SpawnCount)
                {
                    poolTeams.Add(entity).Team = Team.Red;
                    colorC.DefaultColor = Color.red;
                }

                else
                {
                    poolTeams.Add(entity).Team = Team.Blue;
                    colorC.DefaultColor = Color.blue;
                }
            }
        }
    }
}
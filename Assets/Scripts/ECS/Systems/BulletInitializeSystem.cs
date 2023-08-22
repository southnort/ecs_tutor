using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Yrr.Utils;

namespace Game.ECS
{
    internal readonly struct BulletInitializeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ShotComponent>> _filter;
        private readonly EcsCustomInject<PrefabsConfig> _prefabsConfig;
        private readonly EcsCustomInject<MoveConfig> _moveConfig;
        private readonly EcsCustomInject<ShootingConfig> _shootingConfig;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            var poolTeams = world.GetPool<TeamComponent>();
            var poolViews = world.GetPool<GameObjectComponent>();

            var poolSpawnReq = world.GetPool<SpawnRequireComponent>();
            var poolMoving = world.GetPool<MoveInPointComponent>();
            var poolDamage = world.GetPool<DamageComponent>();
            var poolDestination = world.GetPool<DestroyOnDestinationReachedComponent>();


            foreach (var entity in _filter.Value)
            {
                var bullet = world.NewEntity();

                ref var spawnReqC = ref poolSpawnReq.Add(bullet);
                spawnReqC.PrefabPath = _prefabsConfig.Value.BulletPrefabPath;
                spawnReqC.SpawnPos = poolViews.Get(entity).GameObject.transform.position;

                poolTeams.Add(bullet).Team = poolTeams.Get(entity).Team;
                poolDamage.Add(bullet).Damage = _shootingConfig.Value.Damage;

                ref var moveC = ref poolMoving.Add(bullet);
                moveC.TargetPoint = poolMoving.Get(entity).TargetPoint.GetRandomCoordinatesAroundPoint(5f);
                moveC.Speed = _moveConfig.Value.BulletSpeed;

                poolViews.Add(bullet);
                poolDestination.Add(bullet);

                world.GetPool<ShotComponent>().Del(entity);
            }
        }
    }
}

using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS
{
    internal readonly struct ShootingCooldownSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<ShootCooldownComponent>> _filter;
        private readonly EcsPoolInject<ShootCooldownComponent> _shootingP;
        private readonly EcsCustomInject<ShootingConfig> _config;
        private readonly EcsWorldInject _world;

        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ResetShootCooldown(entity);
            }
        }

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var cooldownC = ref _shootingP.Value.Get(entity);
                cooldownC.ShootCooldown -= Time.deltaTime;


                if (cooldownC.ShootCooldown <= 0)
                {
                    ResetShootCooldown(entity);
                    var poolShots = _world.Value.GetPool<ShotComponent>();
                    poolShots.Add(entity);
                }
            }
        }

        private void ResetShootCooldown(int entity)
        {
            ref var shootC = ref _shootingP.Value.Get(entity);
            shootC.ShootCooldown = Random.Range(_config.Value.ShootInterval.x, _config.Value.ShootInterval.y);
        }
    }
}

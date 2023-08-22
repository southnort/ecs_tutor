using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS
{
    internal readonly struct DamageSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filter;
        private readonly EcsPoolInject<HitComponent> _poolHits;
        private readonly EcsPoolInject<DamageComponent> _poolDamage;
        private readonly EcsPoolInject<HealthComponent> _poolHealth;
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var hitC = ref _poolHits.Value.Get(entity);
                if (hitC.FirstCollide && hitC.SecondCollide)
                {
                    var entitiesCollide =
                       PackerEntityUtils.UnpackEntities(_world.Value, hitC.FirstCollide.Entity, hitC.SecondCollide.Entity);

                    ref var damageC = ref _poolDamage.Value.Get(entitiesCollide.Item1);
                    ref var healthC = ref _poolHealth.Value.Get(entitiesCollide.Item2);
                    healthC.Health -= damageC.Damage;

                    var viewPool = _world.Value.GetPool<GameObjectComponent>();
                    var bulletView = viewPool.Get(entitiesCollide.Item1);
                    Object.DestroyImmediate(bulletView.GameObject);
                    _world.Value.DelEntity(entitiesCollide.Item1);

                }

                _poolHits.Value.Del(entity);
                _world.Value.DelEntity(entity);
            }
        }
    }
}

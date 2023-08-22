using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.ECS
{
    internal readonly struct HitPointsObserveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HealthComponent>> _filter;
        private readonly EcsPoolInject<HealthComponent> _poolHealth;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var health = _poolHealth.Value.Get(entity);
                if (health.Health <= 0)
                {
                    systems.GetWorld().GetPool<DestroyComponent>().Add(entity);
                }
            }
        }
    }
}

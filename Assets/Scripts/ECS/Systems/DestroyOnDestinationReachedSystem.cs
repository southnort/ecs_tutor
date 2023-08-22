using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS
{
    internal readonly struct DestroyOnDestinationReachedSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DestinationReachedComponent, DestroyOnDestinationReachedComponent>> _filter;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolDestroy = systems.GetWorld().GetPool<DestroyComponent>();
            foreach (var entity in _filter.Value)
            {
                poolDestroy.Add(entity);
            }
        }
    }
}

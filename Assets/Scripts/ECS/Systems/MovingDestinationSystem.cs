using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS
{
    internal readonly struct MovingDestinationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GameObjectComponent, MoveInPointComponent>> _filter;
        private readonly EcsPoolInject<MoveInPointComponent> _poolMoving;
        private readonly EcsPoolInject<GameObjectComponent> _poolView;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var mc = ref _poolMoving.Value.Get(entity);
                ref var view = ref _poolView.Value.Get(entity);

                var sqrDist = (view.GameObject.transform.position - mc.TargetPoint).sqrMagnitude;
                if (sqrDist < 0.1f)
                {
                    systems.GetWorld().GetPool<DestinationReachedComponent>().Add(entity);
                }
            }
        }
    }
}

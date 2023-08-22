using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS
{
    internal readonly struct SwapDestinationPointSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DestinationReachedComponent, WayPointComponent>> _filter;
        private readonly EcsPoolInject<DestinationReachedComponent> _poolDestinations;
        private readonly EcsPoolInject<WayPointComponent> _poolWayPoints;
        private readonly EcsPoolInject<MoveInPointComponent> _poolMoveInPoints;
        private readonly EcsPoolInject<GameObjectComponent> _poolGameObjects;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var mc = ref _poolMoveInPoints.Value.Get(entity);
                var wc = _poolWayPoints.Value.Get(entity);
                var view = _poolGameObjects.Value.Get(entity);

                if ((view.GameObject.transform.position - wc.StartPos).sqrMagnitude < 0.1f)
                    mc.TargetPoint = wc.EndPos;
                else if ((view.GameObject.transform.position - wc.EndPos).sqrMagnitude < 0.1f)
                    mc.TargetPoint = wc.StartPos;

                _poolDestinations.Value.Del(entity);
            }
        }
    }
}

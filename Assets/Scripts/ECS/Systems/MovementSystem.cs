using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS
{
    internal readonly struct MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveInPointComponent>> _filter;
        private readonly EcsPoolInject<MoveInPointComponent> _poolMoving;
        private readonly EcsPoolInject<GameObjectComponent> _poolView;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var mc = ref _poolMoving.Value.Get(entity);
                ref var view = ref _poolView.Value.Get(entity);

                var pos = Vector3.MoveTowards(view.GameObject.transform.position, mc.TargetPoint, mc.Speed * Time.deltaTime);
                view.GameObject.transform.position = pos;
            }
        }
    }
}

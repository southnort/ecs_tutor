using Game.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game
{
    internal readonly struct DestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DestroyComponent>> _filter;
        private readonly EcsPoolInject<GameObjectComponent> _poolView;
        private readonly EcsWorldInject _world;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var view = ref _poolView.Value.Get(entity);

                Object.DestroyImmediate(view.GameObject);
                _world.Value.DelEntity(entity);
            }
        }
    }
}

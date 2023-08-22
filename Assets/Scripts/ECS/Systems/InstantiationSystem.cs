using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS
{
    internal readonly struct InstantiationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnRequireComponent, GameObjectComponent>> _filter;
        private readonly EcsWorldInject _world;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolSpawnReq = _filter.Pools.Inc1;
            var poolGo = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                var spawnReqC = poolSpawnReq.Get(entity);
                ref var goC = ref poolGo.Get(entity);

                var go = Object.Instantiate(Resources.Load<EcsMonoObject>(spawnReqC.PrefabPath),
                   spawnReqC.SpawnPos, Quaternion.identity);
                go.Init(_world.Value);
                go.PackEntity(entity);
                goC.GameObject = go.gameObject;

                var poolColor = _world.Value.GetPool<ColorComponent>();
                if (poolColor.Has(entity))
                {
                    ref var colorC = ref poolColor.Get(entity);
                    colorC.Renderer = go.GetComponent<MeshRenderer>();
                    colorC.Renderer.material.color = colorC.DefaultColor;
                }

                poolSpawnReq.Del(entity);
            }
        }
    }
}

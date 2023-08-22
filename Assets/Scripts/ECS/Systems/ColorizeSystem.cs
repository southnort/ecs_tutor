using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Threading.Tasks;
using UnityEngine;


namespace Game.ECS
{
    internal readonly struct ColorizeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filter;
        private readonly EcsPoolInject<HitComponent> _poolHits;
        private readonly EcsPoolInject<ColorComponent> _poolColor;
        private readonly EcsWorldInject _world;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var hitC = _poolHits.Value.Get(entity);
                var entitiesCollide =
                       PackerEntityUtils.UnpackEntities(_world.Value, hitC.FirstCollide.Entity, hitC.SecondCollide.Entity);
                ref var cc = ref _poolColor.Value.Get(entitiesCollide.Item2);
                ColorizeCor(cc.DefaultColor, cc.TempoColor, cc.Renderer.material);
            }
        }

        private async void ColorizeCor(Color c1, Color c2, Material mat)
        {
            mat.color = c2;
            await Task.Delay(500);
            if (mat)
                mat.color = c1;
        }
    }
}

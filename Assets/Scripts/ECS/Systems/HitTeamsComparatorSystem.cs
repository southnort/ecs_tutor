using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.ECS
{
    internal readonly struct HitTeamsComparatorSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<HitComponent>> _filter;
        private readonly EcsPoolInject<HitComponent> _poolHits;
        private readonly EcsPoolInject<TeamComponent> _poolTeam;
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
                    var team1 = _poolTeam.Value.Get(entitiesCollide.Item1);
                    var team2 = _poolTeam.Value.Get(entitiesCollide.Item2);

                    if (team1.Team != team2.Team) continue;
                    _poolHits.Value.Del(entity);
                    _world.Value.DelEntity(entity);
                }
            }
        }
    }
}

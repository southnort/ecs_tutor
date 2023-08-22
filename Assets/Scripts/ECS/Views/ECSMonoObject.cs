using Leopotam.EcsLite;
using UnityEngine;


namespace Game.ECS
{
    internal abstract class EcsMonoObject : MonoBehaviour
    {
        public EcsPackedEntity Entity { get; private set; }
        protected EcsWorld World;

        public void Init(EcsWorld world)
        {
            World = world;
        }

        public void PackEntity(int entity)
        {
            Entity = World.PackEntity(entity);
        }

        protected virtual void OnTriggerAction(EcsMonoObject firstCollide, EcsMonoObject secondCollide)
        {
            if (World == null) return;

            var entity = World.NewEntity();
            var poolHitC = World.GetPool<HitComponent>();
            ref var hitC = ref poolHitC.Add(entity);
            hitC.FirstCollide = firstCollide;
            hitC.SecondCollide = secondCollide;
        }
    }
}

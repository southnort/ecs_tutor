using Leopotam.EcsLite;
using System;


namespace Game
{
    internal struct PackerEntityUtils
    {
        public static (int, int) UnpackEntities(EcsWorld world, EcsPackedEntity ent1, EcsPackedEntity ent2)
        {
            if (ent1.Unpack(world, out var unpacked1) &&
                ent2.Unpack(world, out var unpacked2))
            {
                return (unpacked1, unpacked2);
            }

            throw new InvalidOperationException("Not unpacked!");
        }

        public static int UnpackEntity(EcsWorld world, EcsPackedEntity entity)
        {
            if (entity.Unpack(world, out var result))
                return result;

            throw new InvalidOperationException("Not unpacked!");
        }
    }
}

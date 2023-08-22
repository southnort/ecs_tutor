using UnityEngine;

namespace Game.ECS
{
    internal sealed class BulletView : EcsMonoObject
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<BlockView>(out var collide))
                OnTriggerAction(this, collide);
        }
    }
}

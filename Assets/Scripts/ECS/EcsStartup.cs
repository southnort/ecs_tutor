using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.UnityEditor;
using UnityEngine;

namespace Game.ECS
{
    internal sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _systems;

        [SerializeField] private HealthConfig healthConfig;
        [SerializeField] private MoveConfig moveConfig;
        [SerializeField] private PrefabsConfig prefabsConfig;
        [SerializeField] private ShootingConfig shootingConfig;
        [SerializeField] private SpawnConfig spawnConfig;


        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);


            _systems
                .Add(new EcsWorldDebugSystem())
                .Add(new BlockInitializeSystem())
                .Add(new WayPointSystem())
                .Add(new InstantiationSystem())
                .Add(new MovementSystem())
                .Add(new MovingDestinationSystem())
                .Add(new SwapDestinationPointSystem())
                .Add(new ShootingCooldownSystem())
                .Add(new BulletInitializeSystem())
                .Add(new DestroyOnDestinationReachedSystem())
                .Add(new HitTeamsComparatorSystem())
                .Add(new ColorizeSystem())
                .Add(new DamageSystem())
                .Add(new HitPointsObserveSystem())
                .Add(new DestroySystem())


                .Inject()
                .Inject(healthConfig)
                .Inject(moveConfig)
                .Inject(prefabsConfig)
                .Inject(shootingConfig)
                .Inject(spawnConfig)

                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}

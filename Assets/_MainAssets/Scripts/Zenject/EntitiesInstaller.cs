using Zenject;
using UnityEngine;

public class EntitiesInstaller : MonoInstaller
{
    [Header("Counter")]
    [SerializeField] private Counter _counterPrefab;
    [SerializeField] private CounterUpgrader _counterUpgraderPrefab;
    [SerializeField] private CounterDisplay _counterDisplayPrefab;
    [Space]
    [Header("Enemy")]
    [SerializeField] private EnemyKeeper _enemyGiverPrefab;
    [SerializeField] private EnemyDisplay _enemyDisplayPrefab;

    public override void InstallBindings()
    {
        BindCounter();
        BindEnemy();        
    }

    private void BindCounter()
    {
        Container.Bind<CounterSaver>()
                 .AsSingle()
                 .NonLazy();

        Container.BindInstance(_counterUpgraderPrefab).AsSingle();

        Container.BindInstance(_counterPrefab).AsSingle();

        Container.BindInstance(_counterDisplayPrefab);
    }

    private void BindEnemy()
    {
        Container.Bind<EnemyKeeper>()
                 .FromInstance(_enemyGiverPrefab)
                 .AsSingle();
        
        Container.Bind<EnemyDisplay>()
                 .FromInstance(_enemyDisplayPrefab)
                 .AsSingle();

        Container.Bind<EnemySaver>()
                 .AsSingle()
                 .NonLazy();
    }
}
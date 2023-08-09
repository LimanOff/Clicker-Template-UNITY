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
    [Space]
    [Header("AutoSaver")]
    [SerializeField] private AutoSaver _autoSaverPrefab;

    public override void InstallBindings()
    {
        BindSavers();
        BindCounter();
        BindEnemy();
    }

    private void BindCounter()
    {
        Container.BindInstance(_counterUpgraderPrefab).AsSingle();

        Container.BindInstance(_counterPrefab).AsSingle();

        Container.BindInstance(_counterDisplayPrefab);
    }

    private void BindEnemy()
    {
        Container.BindInstance(_enemyGiverPrefab)
                 .AsSingle();
        
        Container.BindInstance(_enemyDisplayPrefab)
                 .AsSingle();        
    }

    private void BindSavers()
    {
        Container.BindInstance(_autoSaverPrefab).AsSingle();
        
        Container.Bind<EnemySaver>()
                 .AsSingle()
                 .NonLazy();

        Container.Bind<CounterSaver>()
                 .AsSingle()
                 .NonLazy();
    }
}
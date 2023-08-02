using Zenject;
using UnityEngine;

public class EntitiesInstaller : MonoInstaller
{
    [SerializeField] private Counter _counterPrefab;
    [SerializeField] private CounterDisplay _counterDisplayPrefab;

    [SerializeField] private EnemyKeeper _enemyGiverPrefab;
    [SerializeField] private EnemyDisplay _enemyDisplayPrefab;
    [SerializeField] private EnemySaver _enemySaverPrefab;

    public override void InstallBindings()
    {
        BindCounter();
        BindEnemy();        
    }

    private void BindCounter()
    {
        Container.Bind<Counter>()
                 .FromInstance(_counterPrefab)
                 .AsSingle();

        Container.Bind<CounterDisplay>()
                 .FromInstance(_counterDisplayPrefab)
                 .AsSingle();
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
                 .FromInstance(_enemySaverPrefab)
                 .AsSingle();
    }
}
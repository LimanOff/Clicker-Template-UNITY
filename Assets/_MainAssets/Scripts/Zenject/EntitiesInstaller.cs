using Zenject;
using UnityEngine;

public class EntitiesInstaller : MonoInstaller
{
    [SerializeField] private Counter _counterPrefab;
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
                 .AsSingle()
                 .NonLazy();
    }
}
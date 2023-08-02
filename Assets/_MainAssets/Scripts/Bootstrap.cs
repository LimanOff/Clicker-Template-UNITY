using YG;
using Zenject;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Inject] private Counter _counter;
    [Inject] private CounterDisplay _counterDisplay;

    [Inject] private EnemyDisplay _enemyDisplay;
    [Inject] private EnemyKeeper _enemyKeeper;
    [Inject] private EnemySaver _enemySaver;

    private void Awake()
    {
        YandexGame.GetDataEvent += InitializeComponents;

        if(YandexGame.SDKEnabled)
        {
            InitializeComponents();
            Debug.Log("<color=yellow>Bootstrap:</color> all components are <color=green>Initialized</color>");
        }
    }

    private void OnDestroy()
    {
        YandexGame.GetDataEvent -= InitializeComponents;
    }

    private void InitializeComponents()
    {
        _counter.Initialize();
        _counterDisplay.Initialize();

        _enemySaver.Initialize();
        _enemyKeeper.Initialize();
        _enemyDisplay.Initialize();
    }
}

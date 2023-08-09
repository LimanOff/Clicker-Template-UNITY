using YG;
using Zenject;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [Inject] private AutoSaver _autoSaver;

    [Inject] private CounterSaver _counterSaver;
    [Inject] private CounterUpgrader _counterUpgrader;
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
        }
    }

    private void OnDestroy()
    {
        YandexGame.GetDataEvent -= InitializeComponents;
    }

    private void InitializeComponents()
    {
        InitializeCounter();
        InitializeEnemy();

        _autoSaver.Initialize();
    }

    private void InitializeCounter()
    {
        _counterSaver.Initialize();
        _counterUpgrader.Initialize();
        _counterDisplay.Initialize();
    }

    private void InitializeEnemy()
    {
        _enemySaver.Initialize();
        _enemyDisplay.Initialize();
        _enemyKeeper.Initialize();
    }
}

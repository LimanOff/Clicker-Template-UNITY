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

    [Inject] private WinHandler _winHandler;


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
        _winHandler.Initialize();
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
        _enemyKeeper.Initialize();
        _enemyDisplay.Initialize();
    }
}

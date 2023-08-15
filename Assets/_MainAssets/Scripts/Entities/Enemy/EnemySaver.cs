using System;
using YG;
using Zenject;

public class EnemySaver : ISaveable, ILoadable, IInitializable
{
    private EnemyKeeper _enemyKeeper;
    private EnemyDisplay _enemyDisplay;

    [Inject]
    public EnemySaver(EnemyKeeper enemyKeeper, EnemyDisplay enemyDisplay)
    {
        _enemyKeeper = enemyKeeper;
        _enemyDisplay = enemyDisplay;
    }

    public void Initialize()
    {
        _enemyDisplay.EnemyKilled += SaveData;
        LoadData();
    }

    ~EnemySaver()
    {
        _enemyDisplay.EnemyKilled -= SaveData;
    }
    
    public void SaveData()
    {
        if(_enemyKeeper.CountOfEnemies != 0)
        {
            YandexGame.savesData.CountOfEnemies = _enemyKeeper.CountOfEnemies;

            YandexGame.SaveProgress();
        }
    }

    public void LoadData()
    {
        if(YandexGame.savesData.CountOfEnemies != 0)
        {
            _enemyKeeper.SetEnemies(YandexGame.savesData.CountOfEnemies);
        }
    }
}

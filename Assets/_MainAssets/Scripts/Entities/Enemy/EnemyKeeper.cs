using YG;
using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class EnemyKeeper : MonoBehaviour, IInitializable
{
    public delegate void NoMoreEnemiesHandler();
    public event NoMoreEnemiesHandler NoMoreEnemies;

    private EnemyDisplay _enemyDisplay;

    [SerializeField] private List<EnemyData> _enemies;
    public int CountOfEnemies {get; private set;}

    [Inject]
    private void Construct(EnemyDisplay enemyDisplay)
    {
        _enemyDisplay = enemyDisplay;
    }

    public void Initialize()
    {
        _enemyDisplay.EnemyKilled += RemoveKilledEnemy;
    }

    private void OnDestroy()
    {
        _enemyDisplay.EnemyKilled -= RemoveKilledEnemy;
    }

    public bool CanGetEnemy()
    {
        if(_enemies.Count != 0)
        {
            return true;
        }
        
        NoMoreEnemies?.Invoke();
        return false;
    }

    public EnemyData GetEnemy()
    {
        EnemyData enemy = _enemies[_enemies.Count-1];
        return enemy;            
    }

    public void SetEnemies(int CountOfEnemies)
    {
        var enemiesCopy = new List<EnemyData>(CountOfEnemies);

        for(int index = 0; index < enemiesCopy.Capacity; index++)
        {
            enemiesCopy.Add(_enemies[index]);
        }

        _enemies = enemiesCopy;
    }

    private void RemoveKilledEnemy()
    {
        if(_enemies.Count != 1)
        {
            _enemies.RemoveAt(_enemies.Count-1);
            CountOfEnemies = _enemies.Count;
        }
        else
        {
            YandexGame.savesData.IsGameWasFinished = true;
            YandexGame.SaveProgress();
            NoMoreEnemies?.Invoke();
        }
    }
}

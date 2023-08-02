using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class EnemyKeeper : MonoBehaviour, IInitializable
{
    public delegate void NoMoreEnemiesHandler();
    public event NoMoreEnemiesHandler NoMoreEnemies;

    private EnemyDisplay _enemyDisplay;
    private EnemySaver _enemySaver;

    private bool _wasPreviousEnemyKilled;

    [SerializeField] private List<EnemyData> _enemies;

    [Inject]
    private void Construct(EnemyDisplay enemyDisplay)
    {
        _enemyDisplay = enemyDisplay;
    }

    public void Initialize()
    {
        _enemyDisplay.EnemyKilled += RemoveKilledEnemy;
        Debug.Log("<color=yellow>EnemyKeeper</color> is <color=green>Initialize</color>");
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
        if(_enemies.Count != 0)
        {
            EnemyData enemy = _enemies[0];
            return enemy;
        }
        NoMoreEnemies?.Invoke();
        return null;
    }

    public List<EnemyData> GetAllEnemies() => _enemies;

    public void SetEnemies(List<EnemyData> enemies)
    {
        _enemies = enemies;
    }

    private void RemoveKilledEnemy()
    {
        _enemies.RemoveAt(0);
    }
}

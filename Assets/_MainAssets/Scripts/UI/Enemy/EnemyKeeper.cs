using Zenject;
using UnityEngine;
using System.Collections.Generic;

public class EnemyKeeper : MonoBehaviour, IInitializable
{
    public delegate void NoMoreEnemiesHandler();
    public event NoMoreEnemiesHandler NoMoreEnemies;

    private EnemyDisplay _enemyDisplay;

    [SerializeField] private List<EnemyData> _enemies;

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
        EnemyData enemy = _enemies[0];
        return enemy;            
    }

    public List<EnemyData> GetAllEnemies() => _enemies;

    public void SetEnemies(List<EnemyData> enemies)
    {
        _enemies = enemies;
    }

    private void RemoveKilledEnemy()
    {
        if(_enemies.Count != 0)
        {
            _enemies.RemoveAt(0);
        }
    }
}

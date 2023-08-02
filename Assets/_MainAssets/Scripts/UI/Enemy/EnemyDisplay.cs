using Zenject;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(EnemyKeeper))]
public class EnemyDisplay : MonoBehaviour, IInitializable
{
    public delegate void EnemyKilledHandler();
    public event EnemyKilledHandler EnemyKilled;

    private Slider _healthSlider;
    private EnemyData _currentEnemy;
    private EnemyKeeper _enemyKeeper;
    [Inject(Id = "UI/ClickButtonImage")] private Image _enemyAvatar;
    private Counter _counter;

    [Inject]
    private void Construct(Slider enemyLifeSlider, EnemyKeeper enemyGiver,Counter counter)
    {
        _healthSlider = enemyLifeSlider;
        _enemyKeeper = enemyGiver;
        _counter = counter;
    }

    public void Initialize()
    {
        GetEnemy();

        _counter.CountChanged += ReduceHealthSliderValue;
        _enemyKeeper.NoMoreEnemies += () => _counter.CountChanged -= ReduceHealthSliderValue; 
        EnemyKilled += GetEnemy;
        
        Debug.Log("<color=yellow>EnemyDisplay</color> is <color=green>Initialize</color>");
    }

    private void OnDestroy()
    {
        _counter.CountChanged -= ReduceHealthSliderValue;
        EnemyKilled -= GetEnemy;
    }

    private void ReduceHealthSliderValue(ulong damage)
    {
        int minSliderValue = 0;
        
        _healthSlider.value = _healthSlider.value - damage < minSliderValue
                              ? minSliderValue
                              : _healthSlider.value - damage;

        if(_healthSlider.value == 0)
        {
            EnemyKilled?.Invoke();
        }
    }

    private void GetEnemy()
    {
        if(_enemyKeeper.CanGetEnemy())
        {
            _currentEnemy = _enemyKeeper.GetEnemy();

            _healthSlider.maxValue = _currentEnemy.MaxHealth;
            _healthSlider.value = _currentEnemy.MaxHealth;
            _enemyAvatar.sprite = _currentEnemy.Avatar;
        }
    }
}

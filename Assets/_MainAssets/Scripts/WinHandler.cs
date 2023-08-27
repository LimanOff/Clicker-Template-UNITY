using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class WinHandler : IInitializable
{
    private EnemyKeeper _enemyKeeper;

    [Inject(Id="Panels/WinPanel")] private GameObject _winPanel;

    [Inject]
    public WinHandler(EnemyKeeper enemyKeeper)
    {
        _enemyKeeper = enemyKeeper;
    }

    public void Initialize()
    {
        _enemyKeeper.NoMoreEnemies += () => _winPanel.Activate();
    }

    public void OnDestroy()
    {
        _enemyKeeper.NoMoreEnemies -= () => _winPanel.Activate();
    }
}

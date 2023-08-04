using YG;
using Zenject;
using UnityEngine;
public class CounterSaver : ISaveable, ILoadable, IInitializable
{
    private Counter _counter;
    private CounterUpgrader _counterUpgrader;

    [Inject]
    public CounterSaver(Counter counter, CounterUpgrader counterUpgrader)
    {
        _counter = counter;
        _counterUpgrader = counterUpgrader;
    }

    public void Initialize()
    {
        LoadData();
    }

    [ContextMenu("SaveData")]
    public void SaveData()
    {
        YandexGame.savesData.Count = _counter.Count;
        YandexGame.savesData.CountMultiplier = _counter.CountMultiplier;
        YandexGame.savesData.UpgradeCost = _counterUpgrader.UpgradeCost;
        
        YandexGame.SaveProgress();
    }

    [ContextMenu("LoadData")]
    public void LoadData()
    {
        ulong countMultiplierStartValue = 1;
        ulong upgradeCostStartValue = 50;

        _counter.Count = YandexGame.savesData.Count;
        _counter.CountMultiplier = YandexGame.savesData.CountMultiplier == 0 ? countMultiplierStartValue : YandexGame.savesData.CountMultiplier;
        _counterUpgrader.UpgradeCost = YandexGame.savesData.UpgradeCost == 0 ? upgradeCostStartValue : YandexGame.savesData.UpgradeCost;

        _counter.CountChanged?.Invoke(_counter.Count);
        _counterUpgrader.CountMultiplierChanged?.Invoke(_counter.CountMultiplier);
        _counterUpgrader.UpgradeCostChanged?.Invoke(_counterUpgrader.UpgradeCost);
    }
}

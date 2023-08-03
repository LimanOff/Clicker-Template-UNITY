using YG;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class Counter : MonoBehaviour, ISaveable, ILoadable, IInitializable
{
    public delegate void CountChangedHandler(ulong count);
    public event CountChangedHandler CountChanged;

    public delegate void CountMultiplierChangedHandler(ulong countMultiplier);
    public event CountMultiplierChangedHandler CountMultiplierChanged;

    public delegate void UpgradeCostChangedHandler(ulong upgradeCost);
    public event UpgradeCostChangedHandler UpgradeCostChanged;

    public delegate void DontHaveEnoughCountForUpgradeHandler(ulong difference);
    public event DontHaveEnoughCountForUpgradeHandler DontHaveEnoughCountForUpgrade;

    [field: SerializeField] public ulong Count {get; private set;}

    [field: SerializeField] public ulong CountMultiplier {get; private set;}

    [field: SerializeField] public ulong UpgradeCost {get; private set;}

    public void Initialize()
    {
        LoadData();
        
        Debug.Log("<color=yellow>Counter</color> is <color=green>initialized</color>");
    }

    [ContextMenu("SaveData")]
    public void SaveData()
    {
        YandexGame.savesData.Count = Count;
        YandexGame.savesData.CountMultiplier = CountMultiplier;
        YandexGame.savesData.UpgradeCost = UpgradeCost;
        
        YandexGame.SaveProgress();
    }

    [ContextMenu("LoadData")]
    public void LoadData()
    {
        ulong countMultiplierStartValue = 1;
        ulong upgradeCostStartValue = 50;

        Count = YandexGame.savesData.Count;
        CountMultiplier = YandexGame.savesData.CountMultiplier == 0 ? countMultiplierStartValue : YandexGame.savesData.CountMultiplier;
        UpgradeCost = YandexGame.savesData.UpgradeCost == 0 ? upgradeCostStartValue : YandexGame.savesData.UpgradeCost;

        CountChanged?.Invoke(Count);
        CountMultiplierChanged?.Invoke(CountMultiplier);
        UpgradeCostChanged?.Invoke(UpgradeCost);
    }

    public void IncrementCounter()
    {
        Count += CountMultiplier;

        CountChanged?.Invoke(Count);
    }

    public void UpgradeCounterMultiplier()
    {
        if(Count >= UpgradeCost)
        {
            Count -= UpgradeCost;
            CountMultiplier *= 2;
            UpgradeCost *= 2;

            CountChanged?.Invoke(Count);
            CountMultiplierChanged?.Invoke(CountMultiplier);
        }
        else
        {
            DontHaveEnoughCountForUpgrade?.Invoke(UpgradeCost-Count);
        }
    }
}

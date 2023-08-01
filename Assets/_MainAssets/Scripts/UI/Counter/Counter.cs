using YG;
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class Counter : MonoBehaviour, ISaveable
{
    public delegate void CountChangedHandler(ulong count);
    public event CountChangedHandler CountChanged;

    public delegate void CountMultiplierChangedHandler(ulong countMultiplier);
    public event CountMultiplierChangedHandler CountMultiplierChanged;

    public delegate void DontHaveEnoughCountForUpgradeHandler(ulong difference);
    public event DontHaveEnoughCountForUpgradeHandler DontHaveEnoughCountForUpgrade;

    [field: SerializeField] public ulong Count {get; private set;}

    public ulong CountMultiplier {get; private set;}

    public ulong UpgradeCost {get; private set;}

    public void Initialize()
    {
        ulong countMultiplierStartValue = 1;
        ulong upgradeCostStartValue = 2;

        Count = YandexGame.savesData.Count;
        CountMultiplier = YandexGame.savesData.CountMultiplier == 0 ? countMultiplierStartValue : YandexGame.savesData.CountMultiplier;
        UpgradeCost = YandexGame.savesData.UpgradeCost == 0 ? upgradeCostStartValue : YandexGame.savesData.UpgradeCost; 
        
        Debug.Log("<color=yellow>Counter</color> is <color=green>initialized</color>");
    }

    public void SaveData()
    {
        YandexGame.savesData.Count = Count;
        YandexGame.savesData.CountMultiplier = CountMultiplier;
        YandexGame.savesData.UpgradeCost = UpgradeCost;
        
        YandexGame.SaveProgress();
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
            CountMultiplier *= 2;
            UpgradeCost *= 2;
            Count -= UpgradeCost;

            CountMultiplierChanged?.Invoke(CountMultiplier);
        }
        else
        {
            DontHaveEnoughCountForUpgrade?.Invoke(UpgradeCost-Count);
        }
    }
}

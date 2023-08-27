using YG;
using System;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CounterUpgrader : MonoBehaviour, IInitializable
{
    public delegate void UpgradeCostChangedHandler(ulong upgradeCost);
    public UpgradeCostChangedHandler UpgradeCostChanged;

    public delegate void UpgradeHappenedHandler();
    public event UpgradeHappenedHandler UpgradeHappened;

    public delegate void CountMultiplierChangedHandler(ulong countMultiplier);
    public CountMultiplierChangedHandler CountMultiplierChanged;

    public delegate void DontHaveEnoughCountForUpgradeHandler(ulong difference);
    public event DontHaveEnoughCountForUpgradeHandler DontHaveEnoughCountForUpgrade;
    
    public ulong UpgradeCost;
    public bool IsAdUpgradeActive {get; private set;}

    private Coroutine _timer;
    private Counter _counter;
    private CounterDisplay _counterDisplay;

    [SerializeField] private Button _sender;
    private Button _senderAD;
    [SerializeField] private Text _priceToUpgrade;

    private bool _wasADShown;

    [Inject]
    private void Construct(Counter counter)
    {
        _counter = counter;
    }

    public void Initialize()
    {
        YandexGame.ErrorVideoEvent += () => _wasADShown = false;

        YandexGame.RewardVideoEvent += (int id) => 
        {
            _wasADShown = true;
        };

        YandexGame.CloseVideoEvent += () => UpgradeCounterMultiplierForAD(_senderAD);
    }

    private void OnDestroy()
    {
        YandexGame.ErrorVideoEvent -= () => _wasADShown = false;

        YandexGame.RewardVideoEvent -= (int id) => 
        {
            _wasADShown = true;
        };

        YandexGame.CloseVideoEvent -= () => UpgradeCounterMultiplierForAD(_senderAD);
    }

    private void Update()
    {
        _priceToUpgrade.text = CounterDisplay.GetFormatText(UpgradeCost);

        if(_counter.Count < UpgradeCost)
        {
            _sender.interactable = false;
        }
        else
        {
            _sender.interactable = true;
        }
    }

    public void UpgradeCounterMultiplier()
    {
        if(_counter.Count >= UpgradeCost)
        {
            _counter.Count -= UpgradeCost;
            _counter.CountMultiplier *= 2;
            UpgradeCost *= 2;

            _counter.CountChanged?.Invoke(_counter.Count);
            UpgradeCostChanged?.Invoke(UpgradeCost);
            CountMultiplierChanged?.Invoke(_counter.CountMultiplier);
            UpgradeHappened?.Invoke();
        }
        else
        {
            DontHaveEnoughCountForUpgrade?.Invoke(UpgradeCost-_counter.Count);
        }
    }

    public void RequestAD(Button senderAD)
    {
        YandexGame.RewVideoShow(0);
        _senderAD = senderAD;
    }

    private void UpgradeCounterMultiplierForAD(Button sender)
    {        
        if(_timer != null)
        {
            StopCoroutine(_timer);
            SenderVisible(true);
        }

        if(_wasADShown)
        {
            SenderVisible(false);

            _timer = StartCoroutine(IncrementCounterMultiplierX20ForAWhile(() => SenderVisible(true)));
        }

        _wasADShown = false;
        
        void SenderVisible(bool isInteractable)
        {
            sender.interactable = isInteractable;
        }
    }

    private IEnumerator IncrementCounterMultiplierX20ForAWhile(Action actionAfterCoroutine)
    {
        int seconds = 20;

        _counter.CountMultiplier *= 20;

        IsAdUpgradeActive = true;
        
        CountMultiplierChanged?.Invoke(_counter.CountMultiplier);

        yield return new WaitForSeconds(seconds);

        IsAdUpgradeActive = false;

        _counter.CountMultiplier /= 20;

        CountMultiplierChanged?.Invoke(_counter.CountMultiplier);
        actionAfterCoroutine();
    }
}


using YG;
using Zenject;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [Inject] private Counter _counter;
    [Inject] private CounterDisplay _counterDisplay;

    private void Awake()
    {
        YandexGame.GetDataEvent += InitializeComponents;

        if(YandexGame.SDKEnabled)
        {
            InitializeComponents();
        }
    }

    private void OnDestroy()
    {
        YandexGame.GetDataEvent -= InitializeComponents;
    }

    private void InitializeComponents()
    {
        _counter.Initialize();
        _counterDisplay.Initialize();
    }
}

using Zenject;
using UnityEngine;

public class EntitiesInstaller : MonoInstaller
{
    [SerializeField] private Counter _counter;
    [SerializeField] private CounterDisplay _counterDisplay;

    public override void InstallBindings()
    {
        Container.Bind<Counter>().FromInstance(_counter).AsSingle();
        Container.Bind<CounterDisplay>().FromInstance(_counterDisplay).AsSingle();
    }
}
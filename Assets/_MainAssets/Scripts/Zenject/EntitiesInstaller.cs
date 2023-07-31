using Zenject;
using UnityEngine;

public class EntitiesInstaller : MonoInstaller
{
    [SerializeField] private Counter _counterPrefab;
    [SerializeField] private CounterDisplay _counterDisplayPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Counter>().FromInstance(_counterPrefab).AsSingle();
        Container.Bind<CounterDisplay>().FromInstance(_counterDisplayPrefab).AsSingle();
    }
}
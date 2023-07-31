using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class UIInstaller : MonoInstaller
{
    [Header("GamePanel")]
    [SerializeField] private Text _counterTextPrefab;
    [SerializeField] private Button _clickButtonPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Text>()
                 .WithId("UI/Counter")
                 .FromInstance(_counterTextPrefab)
                 .AsSingle();

        Container.Bind<Button>()
                 .WithId("UI/ClickButton")
                 .FromInstance(_clickButtonPrefab)
                 .AsSingle();
    }
}
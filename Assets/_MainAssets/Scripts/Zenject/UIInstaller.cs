using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class UIInstaller : MonoInstaller
{
    [Header("GamePanel")]
    public Text Counter;
    public Button ClickButton;

    public override void InstallBindings()
    {
        Container.Bind<Text>().WithId("UI/Counter").FromInstance(Counter).AsSingle();
        Container.Bind<Button>().WithId("UI/ClickButton").FromInstance(ClickButton).AsSingle();
    }
}
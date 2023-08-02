using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class UIInstaller : MonoInstaller
{
    [Header("GamePanel")]
    [SerializeField] private Text _counterTextPrefab;
    [SerializeField] private Button _clickButtonPrefab;
    [SerializeField] private Slider _enemyLifeSliderPrefab;

    [SerializeField] private Image _clickButtonSpritePrefab;

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

        Container.Bind<Image>()
                 .WithId("UI/ClickButtonImage")
                 .FromInstance(_clickButtonSpritePrefab)
                 .AsSingle();
                 
        Container.Bind<Slider>()
                 .FromInstance(_enemyLifeSliderPrefab)
                 .AsSingle();
    }
}
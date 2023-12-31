using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class UIInstaller : MonoInstaller
{
    [Header("Panels")]
    [SerializeField] private GameObject _gamePanelPrefab;
    [SerializeField] private GameObject _leaderboardPanelPrefab;
    [SerializeField] private GameObject _winPanelPrefab;

    [Header("GamePanel")]
    [SerializeField] private Text _counterTextPrefab;
    [SerializeField] private Button _clickButtonPrefab;
    [SerializeField] private Slider _enemyLifeSliderPrefab;

    [Header("GamePanel/Images")]
    [SerializeField] private Image _clickButtonSpritePrefab;
    [SerializeField] private Image _enemyBackgroundImagePrefab;

    public override void InstallBindings()
    {
        BindPanels();
        BindImages();

        Container.Bind<Text>()
                 .WithId("UI/Counter")
                 .FromInstance(_counterTextPrefab)
                 .AsSingle();

        Container.Bind<Button>()
                 .WithId("UI/ClickButton")
                 .FromInstance(_clickButtonPrefab)
                 .AsSingle();
                 
        Container.Bind<Slider>()
                 .FromInstance(_enemyLifeSliderPrefab)
                 .AsSingle();
    }

    private void BindPanels()
    {
        Container.BindInstance(_gamePanelPrefab).WithId("Panels/GamePanel");
        Container.BindInstance(_leaderboardPanelPrefab).WithId("Panels/LeaderboardPanel");
        Container.BindInstance(_winPanelPrefab).WithId("Panels/WinPanel");
    }

    private void BindImages()
    {
        Container.Bind<Image>()
                 .WithId("UI/ClickButtonImage")
                 .FromInstance(_clickButtonSpritePrefab);

        Container.Bind<Image>()
                 .WithId("UI/BackgroundImage")
                 .FromInstance(_enemyBackgroundImagePrefab);
    }
}
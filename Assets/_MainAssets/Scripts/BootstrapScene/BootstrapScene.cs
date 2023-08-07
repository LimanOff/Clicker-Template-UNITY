using YG;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BootstrapScene : MonoBehaviour
{
    private const int _desktopSceneIndex = 1;
    private const int _mobileSceneIndex = 2;

    private void Awake()
    {
        YandexGame.GetDataEvent += IdentifyDevice;
    }

    private void OnDestroy()
    {
        YandexGame.GetDataEvent -= IdentifyDevice;
    }

    private void IdentifyDevice()
    {
        if(YandexGame.EnvironmentData.isDesktop)
        {
            SceneManager.LoadScene(_desktopSceneIndex);
        }

        if(YandexGame.EnvironmentData.isMobile)
        {
            SceneManager.LoadScene(_mobileSceneIndex);
        }
    }
}

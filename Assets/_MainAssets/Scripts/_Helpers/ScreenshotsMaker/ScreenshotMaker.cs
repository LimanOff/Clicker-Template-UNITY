using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotMaker : MonoBehaviour
{
    private int _screenshotsForSession;
    [SerializeField] private string _pathToSavedScreenshots; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            _screenshotsForSession++;
            ScreenCapture.CaptureScreenshot($@"{_pathToSavedScreenshots}\screenshot_{_screenshotsForSession}.png");
        }
    }
}

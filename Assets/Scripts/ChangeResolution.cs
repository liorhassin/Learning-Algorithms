using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeResolution : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    public bool isFullScreen;

    private float _currentRefreshRate;
    //private int _currentResolutionIndex = 0;
    // Start is called before the first frame update
    private void Start()
    {
        isFullScreen = false;
        _currentRefreshRate = Screen.currentResolution.refreshRate;
        SetResolution(0, false);
    }

    private void SetResolution(int resolutionIndex, bool isFullScreen)
    {
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, isFullScreen, (int)_currentRefreshRate);
    }
    
    public void SetFullScreen()
    {
        isFullScreen = !isFullScreen;
        if (isFullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}

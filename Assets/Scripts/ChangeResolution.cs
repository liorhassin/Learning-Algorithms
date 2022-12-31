using System;
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
        SetResolution();
    }

    public void SetResolution()
    {
        String t = resolutionDropdown.options[resolutionDropdown.value].text;
        String[] res = t.Split('x');
        Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), isFullScreen, (int)_currentRefreshRate);
        print("Resolution set to " + res[0] + "x" + res[1]);
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

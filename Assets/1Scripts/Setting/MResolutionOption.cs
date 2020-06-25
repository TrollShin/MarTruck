using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MResolutionOption : MDropdownOption
{
    protected override void InitDropdownOptions()
    {
        List<string> options = new List<string>();
        int value = -1;
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            Resolution resolution = Screen.resolutions[i];
            int currentRefreshRate = Screen.currentResolution.refreshRate;
            options.Add(resolution.ToString());
            if (resolution.width == Screen.currentResolution.width 
                && resolution.height == Screen.currentResolution.height 
                && (currentRefreshRate - 1 <= resolution.refreshRate || resolution.refreshRate <= currentRefreshRate + 1)) //Refresh rate has +-1 margin
                value = i;
        }
        DropdownOption.AddOptions(options);
        DropdownOption.value = value;
        
    }

    protected override void SetOption(int index)
    {
        Resolution resolution = Screen.resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, resolution.refreshRate);
    }
}

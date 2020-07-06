using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MBackgroundVolumeOption : MSliderOption
{
    protected override void InitOption()
    {
        SliderOption.minValue = 0;
        SliderOption.maxValue = 100;
        float value = CSoundManager.GetInstance().BackgroundVolume * 100;
        SliderOption.value = value;
        DisplayText.text = value.ToString();
    }

    protected override void SetOption(float value)
    {
        CSoundManager.GetInstance().BackgroundVolume = value / 100f;
        DisplayText.text = value.ToString();
    }
}

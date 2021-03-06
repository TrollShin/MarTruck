﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MEffectVolumeOption : MSliderOption
{    
    protected override void InitOption()
    {
        SliderOption.minValue = 0;
        SliderOption.maxValue = 100;
        float value = CSoundManager.GetInstance().EffectVolume * 100;
        SliderOption.value = value;
        DisplayText.text = value.ToString();
    }

    protected override void SetOption(float value)
    {
        CSoundManager.GetInstance().EffectVolume = value / 100f;
        DisplayText.text = value.ToString();
    }
}

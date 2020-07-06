using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MSensitivityOption : MSliderOption
{
    protected override void InitOption()
    {
        SliderOption.minValue = 1;
        SliderOption.maxValue = 100;
        float Sensitivity = CGameInputManager.GetInstance().RotationSensitivity;
        SliderOption.value = Sensitivity;
        DisplayText.text = ((int)Sensitivity).ToString();
    }

    protected override void SetOption(float value)
    {
        CGameInputManager.GetInstance().RotationSensitivity = value;
        DisplayText.text = ((int)value).ToString();
    }
}

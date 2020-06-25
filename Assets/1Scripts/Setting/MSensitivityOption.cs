using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MSensitivityOption : MSliderOption
{
    public Text DisplayText;

    protected override void InitOption()
    {
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

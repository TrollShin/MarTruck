using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class MSliderOption : MOption<float>
{
    public Slider SliderOption;
    public TextMeshProUGUI DisplayText;

    // Start is called before the first frame update
    void Start()
    {
        SliderOption.onValueChanged.AddListener(SetOption);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MSliderOption : MOption<float>
{
    public Slider SliderOption;

    // Start is called before the first frame update
    void Start()
    {
        SliderOption.onValueChanged.AddListener(SetOption);
    }
}

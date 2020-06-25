using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MSliderOption : MonoBehaviour
{
    public Slider SliderOption;

    // Start is called before the first frame update
    void Start()
    {
        InitOption();
        SliderOption.onValueChanged.AddListener(SetOption);
    }

    protected abstract void InitOption();

    protected abstract void SetOption(float value);
}

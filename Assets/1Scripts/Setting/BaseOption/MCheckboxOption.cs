using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MCheckboxOption : MonoBehaviour
{
    public Toggle ToggleOption;

    // Start is called before the first frame update
    void Start()
    {
        InitOption();
        ToggleOption.onValueChanged.AddListener(SetOption);
    }

    protected abstract void InitOption();

    protected abstract void SetOption(bool isChecked);
}

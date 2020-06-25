using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MCheckboxOption : MOption<bool>
{
    public Toggle ToggleOption;

    // Start is called before the first frame update
    void Start()
    {
        ToggleOption.onValueChanged.AddListener(SetOption);
    }
}

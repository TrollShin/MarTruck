using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MDropdownOption : MonoBehaviour
{
    public Dropdown DropdownOption;

    // Start is called before the first frame update
    void Start()
    {
        InitDropdownOptions();
        DropdownOption.onValueChanged.AddListener(SetOption);
    }

    protected abstract void InitDropdownOptions();

    protected abstract void SetOption(int index);
}

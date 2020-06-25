﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MDropdownOption : MOption<int>
{
    public Dropdown DropdownOption;

    // Start is called before the first frame update
    void Start()
    {
        DropdownOption.onValueChanged.AddListener(SetOption);
    }
}
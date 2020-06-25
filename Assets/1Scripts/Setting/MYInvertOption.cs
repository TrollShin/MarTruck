using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MYInvertOption : MCheckboxOption
{
    protected override void InitOption()
    {
        EMouseReversal MouseReversal = CGameInputManager.GetInstance().MouseReversal;
        ToggleOption.isOn = MouseReversal.GetHashCode() > 0 ? false : true;
    }

    protected override void SetOption(bool isChecked)
    {
        CGameInputManager.GetInstance().MouseReversal = isChecked ? EMouseReversal.Reverse : EMouseReversal.Basic;
    }
}

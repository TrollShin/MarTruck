using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MScreenModeOption : MDropdownOption
{
    protected override void InitDropdownOptions()
    {
        string[] options = typeof(FullScreenMode).GetEnumNames();
        DropdownOption.AddOptions(options.ToList());
        DropdownOption.value = Array.IndexOf(options, Screen.fullScreenMode.ToString());
    }

    protected override void SetOption(int index)
    {
        Type screenModeType = typeof(FullScreenMode);
        Screen.fullScreenMode = (FullScreenMode)screenModeType.GetEnumValues().GetValue(index);
    }
}

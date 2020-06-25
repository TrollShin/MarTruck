using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MQualityOption : MDropdownOption
{
    protected override void InitOption()
    {
        List<Dropdown.OptionData> Options = new List<Dropdown.OptionData>();
        foreach (string Option in typeof(EGraphicQuality).GetEnumNames())
        {
            Options.Add(new Dropdown.OptionData(Option));
        }
        DropdownOption.AddOptions(Options);
        DropdownOption.value = QualitySettings.GetQualityLevel(); ;
    }

    protected override void SetOption(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}

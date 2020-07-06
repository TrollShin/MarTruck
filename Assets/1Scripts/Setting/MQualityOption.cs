using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MQualityOption : MDropdownOption
{
    protected override void InitOption()
    {
        List<TMP_Dropdown.OptionData> Options = new List<TMP_Dropdown.OptionData>();
        foreach (string Option in typeof(EGraphicQuality).GetEnumNames())
        {
            Options.Add(new TMP_Dropdown.OptionData(Option));
        }
        DropdownOption.AddOptions(Options);
        DropdownOption.value = QualitySettings.GetQualityLevel(); ;
    }

    protected override void SetOption(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}

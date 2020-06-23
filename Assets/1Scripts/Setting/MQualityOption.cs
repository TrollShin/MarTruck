using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MQualityOption : MonoBehaviour
{
    public Dropdown DropdownOption;

    private int _QualityLevel;
    private int QualityLevel
    {
        get { return _QualityLevel; }
        set { QualitySettings.SetQualityLevel(_QualityLevel); }
    }

    private void Awake()
    {
        _QualityLevel = QualitySettings.GetQualityLevel();
    }

    // Start is called before the first frame update
    void Start()
    {
        List<Dropdown.OptionData> Options = new List<Dropdown.OptionData>();
        foreach(string Option in typeof(EGraphicQuality).GetEnumNames())
        {
            Options.Add(new Dropdown.OptionData(Option));
        }
        DropdownOption.AddOptions(Options);
        DropdownOption.value = QualityLevel;
        DropdownOption.onValueChanged.AddListener(SetOption);
    }

    void SetOption(int index)
    {
        QualityLevel = index;
    }
}

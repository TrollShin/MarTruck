using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct SOptionType
{
    public Button button;
    public GameObject Options;
}

public class MSettingFunctionLibrary : MonoBehaviour
{
    public SOptionType[] OptionTypes;
    private GameObject Activated;

    private void Awake()
    {
        foreach (SOptionType OptionType in OptionTypes)
        {
            OptionType.Options.SetActive(false);
            OptionType.button.onClick.AddListener(()=>
            { ShowOptions(OptionType.Options); });
        }

        Activated = OptionTypes[0].Options;
        OptionTypes[0].Options.SetActive(true);
    }

    public void OnClickExit()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    private void ShowOptions(GameObject Options)
    {
        if(Activated != Options)
        {
            Activated.SetActive(false);
            Options.SetActive(true);
            Activated = Options;
        }
    }
}

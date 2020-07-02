using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MStoreUIFunctionLibrary : MonoBehaviour
{
    public SOptionType[] OptionTypes;
    public GameObject Menu;
    private GameObject Activated;

    private void Awake()
    {
        foreach (SOptionType OptionType in OptionTypes)
        {
            OptionType.Options.SetActive(false);
            OptionType.button.onClick.AddListener(() =>
            { ShowOptions(OptionType.Options); });
        }

        Activated = Menu;
        Menu.SetActive(true);
    }

    public void OnClickExit()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        MQuest.LoadEvent = null;
        MQuest.AddEvent = null;
    }

    private void ShowOptions(GameObject Options)
    {
        if (Activated != Options)
        {
            Menu.SetActive(false);
            Activated.SetActive(false);
            Options.SetActive(true);
            Activated = Options;
        }
    }

    public void ActivatedMenu()
    {
        Activated.SetActive(false);
        Activated = Menu;
        Menu.SetActive(true);
    }
}

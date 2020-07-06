using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MStoreUIFunctionLibrary : MonoBehaviour
{
    public GameObject[] Tab;
    private GameObject Activated;

    public delegate void OnUpgradeStore();
    public static OnUpgradeStore StoreEvent;

    private void Awake()
    {
        foreach (GameObject item in Tab)
        {
            item.SetActive(false);
        }

        Activated = Tab[0];
        Activated.SetActive(true);
    }

    public void OnClickExit()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("StoreUI"));
        MQuestController.AddEvent = null;
    }

    public void OnShowQuest()
    {
        Activated.SetActive(false);
        Tab[1].SetActive(true);
        Activated = Tab[1];
    }

    public void OnUpgrade()
    {
        StoreEvent();
    }

    public void OnBackMenu()
    {
        Activated.SetActive(false);
        Activated = Tab[0];
        Activated.SetActive(true);
    }
}

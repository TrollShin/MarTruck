using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CSceneFunctionLibrary
{
    public static void LoadTitle()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    /**
        * Remaining Current Scene, Current Scene must have a Camera for overlay target.
        */
    public static void ShowSettingMenu()
    {
        SceneManager.LoadSceneAsync("Setting", LoadSceneMode.Additive).completed += ShowSettingMenu_completed;
    }

    private static void ShowSettingMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Setting")); // Must be Active Scene for instantiate prefabs
    }

    public static void ShowRepairMenu()
    {
        SceneManager.LoadSceneAsync("RepairShopUI", LoadSceneMode.Additive).completed += ShowRepairMenu_completed;
    }

    private static void ShowRepairMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("RepairShopUI")); // Must be Active Scene for instantiate prefabs
    }

    public static void ShowStoreMenu(List<SQuest> QuestList)
    {
        AsyncOperation asyncHandle = SceneManager.LoadSceneAsync("StoreUI", LoadSceneMode.Additive);
        asyncHandle.completed += ShowStoreMenu_completed;
        asyncHandle.completed += (obj) =>
        {
            MQuest.LoadEvent(QuestList);
        };
    }

    private static void ShowStoreMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("StoreUI")); // Must be Active Scene for instantiate prefabs
    }
}

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
    public static void LoadSceneSafety(string SceneName, Action<AsyncOperation> Completed)
    {
        if (IsSceneLoading(SceneName))
        {
            return;
        }
        if (!SceneManager.GetActiveScene().name.Equals(SceneName))
        {
            SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Additive).completed += Completed;
        }
    }
    public static bool IsSceneLoading(string SceneName)
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (SceneManager.GetSceneAt(i).name.Equals(SceneName))
                return true;
        }
        return false;
    }
    public static void ShowSettingMenu()
    {
        LoadSceneSafety("Setting", ShowSettingMenu_completed);
    }
    private static void ShowSettingMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Setting")); // Must be Active Scene for instantiate prefabs
    }

    public static void ShowRepairMenu()
    {
        LoadSceneSafety("RepairShopUI", ShowRepairMenu_completed);
    }

    private static void ShowRepairMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("RepairShopUI")); // Must be Active Scene for instantiate prefabs
    }

    public static void ShowStoreMenu()
    {
        LoadSceneSafety("StoreUI", ShowStoreMenu_completed);
    }

    private static void ShowStoreMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("StoreUI")); // Must be Active Scene for instantiate prefabs
    }

    public static void ShowStructMenu(MStructure structure)
    {
        LoadSceneSafety("StructureUI", (obj) => 
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("StructureUI"));
            MStructure.StructEvent(structure);
        });
    }

    private static void ShowStructMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("StructureUI")); // Must be Active Scene for instantiate prefabs
    }

    public static void ShowPause()
    {
        if(!SceneManager.GetActiveScene().name.Equals("Pause"))
        {
            SceneManager.LoadSceneAsync("Pause", LoadSceneMode.Additive).completed += ShowPause_completed;
        }
    }

    private static void ShowPause_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Pause"));
    }
}

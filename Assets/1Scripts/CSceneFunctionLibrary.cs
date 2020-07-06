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
        if (!SceneManager.GetActiveScene().buildIndex.Equals(1))
        {
            SceneManager.LoadSceneAsync("Setting", LoadSceneMode.Additive).completed += ShowSettingMenu_completed;
        }
    }

    private static void ShowSettingMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Setting")); // Must be Active Scene for instantiate prefabs
    }

    public static void ShowRepairMenu()
    {
        if(!SceneManager.GetActiveScene().buildIndex.Equals(3))
        {
            SceneManager.LoadSceneAsync("RepairShopUI", LoadSceneMode.Additive).completed += ShowRepairMenu_completed;
        }
    }

    private static void ShowRepairMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("RepairShopUI")); // Must be Active Scene for instantiate prefabs
    }

    public static void ShowStoreMenu()
    {
        if(!SceneManager.GetActiveScene().buildIndex.Equals(2))
        {
            SceneManager.LoadSceneAsync("StoreUI", LoadSceneMode.Additive).completed += ShowStoreMenu_completed;
        }
    }

    private static void ShowStoreMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("StoreUI")); // Must be Active Scene for instantiate prefabs
    }
}

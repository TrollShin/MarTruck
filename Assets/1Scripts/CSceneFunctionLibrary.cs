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

    public static void ShowQuestMenu(List<SQuest> QuestList)
    {
        AsyncOperation asyncHandle = SceneManager.LoadSceneAsync("Quest", LoadSceneMode.Additive);
        asyncHandle.completed += ShowQuestMenu_completed;
        asyncHandle.completed += (obj) =>
        {
            MQuest.LoadEvent(QuestList);
        };
    }

    private static void ShowQuestMenu_completed(AsyncOperation obj)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Quest")); // Must be Active Scene for instantiate prefabs
    }
}

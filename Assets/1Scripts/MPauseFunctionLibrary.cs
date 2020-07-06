using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MPauseFunctionLibrary : MonoBehaviour
{
    public delegate void OnUnLoad();
    public static event OnUnLoad OnUnLoadEvent;

    public delegate void OnExit();
    public static event OnExit OnExitEvent;

    public void OnContinueButtonClick()
    {
        OnUnLoadEvent?.Invoke();
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Pause"));
    }

    public void OnSettingButtonClick()
    {
        CSceneFunctionLibrary.ShowSettingMenu();
        //SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Pause"));
    }

    public void OnExitButtonClick()
    {
        OnExitEvent?.Invoke();
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("Pause"));
    }
}

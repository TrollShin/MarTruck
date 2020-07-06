using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MPause : MonoBehaviour
{
    private bool IsPause = false;

    private void Awake()
    {
        MPauseFunctionLibrary.OnUnLoadEvent += Continue;
        MPauseFunctionLibrary.OnExitEvent += Exit;        
    }

    private void OnDisable()
    {
        MPauseFunctionLibrary.OnUnLoadEvent -= Continue;
        MPauseFunctionLibrary.OnExitEvent -= Exit;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        if (IsPause) return;
        IsPause = true;

        Time.timeScale = 0;
        CSceneFunctionLibrary.ShowPause();
    }

    private void Continue()
    {
        IsPause = false;
        Time.timeScale = 1;
    }

    private void Exit()
    {
        IsPause = false;

        Time.timeScale = 1;
        CSaveGame.GetInstance().Save();
        CSceneFunctionLibrary.LoadTitle();
    }
}

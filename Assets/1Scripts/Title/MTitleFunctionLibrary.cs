using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MTitleFunctionLibrary : MonoBehaviour
{
    public GameObject CheckDataDeleteUI;
    
    private void Awake()
    {
        CheckDataDeleteUI.SetActive(false);
        CQuestDBManager.GetInstance().DBCreate();
        CSaveGame SaveGame = CSaveGame.GetInstance();
        if (!SaveGame.LoadControlSetting())
        {
            SaveGame.Save();
            SaveGame.LoadControlSetting();
        }
        if (!SaveGame.LoadSoundInfo()) 
        {
            SaveGame.Save();
            SaveGame.LoadSoundInfo();
        }
    }

    public void ShowCheckDataDeleteUI()
    {
        CheckDataDeleteUI.SetActive(true);
    }

    public void HideCheckDataDeleteUI()
    {
        CheckDataDeleteUI.SetActive(false);
    }

    public void OnClickNewGame()
    {
        CSaveGame SaveGame = CSaveGame.GetInstance();
        CUserInfo UserInfo = CUserInfo.GetInstance();
        UserInfo = new CUserInfo();
        SaveGame.Save();
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void OnClickContinue()
    {
        CSaveGame SaveGame = CSaveGame.GetInstance();
        if(SaveGame.LoadUserInfo())
        {
            SaveGame.Save();
            SaveGame.LoadUserInfo();
            Debug.Log(CUserInfo.GetInstance().Money);
        }

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void OnClickSetting()
    {
        CSceneFunctionLibrary.ShowSettingMenu();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
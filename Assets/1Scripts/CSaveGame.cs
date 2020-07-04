using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public enum ESaveFileName
{
    ControlSettingSaveData = 0,
    SoundSettingSaveData = 1,
    UserInfoSaveData = 2,
    CurrentQuestsSaveData = 3,
}

public class CSaveGame
{
    private static CSaveGame instance;

    private string FilePath = Application.persistentDataPath;

    private StringBuilder StrBuilder = new StringBuilder();

    public static CSaveGame GetInstance()
    {
        if(instance == null)
        {
            instance = new CSaveGame();
        }

        return instance;
    }

    public void Save()
    {
        StrBuilder.Clear();

        //ESaveFileName 순으로 초기화
        string[] json = new string[4];

        json[0] = JsonUtility.ToJson(CGameInputManager.GetInstance());        

        StrBuilder.Append(CSoundManager.GetInstance().BackgroundVolume.ToString());
        StrBuilder.Append(",");
        StrBuilder.Append(CSoundManager.GetInstance().EffectVolume.ToString());

        json[1] = StrBuilder.ToString();
        json[2] = JsonUtility.ToJson(CUserInfo.GetInstance());
        json[3] = JsonUtility.ToJson(CUserInfo.GetInstance().QuestLst);

        for(int i = 0; i < json.Length; i++)
        {
            ESaveFileName FileName = (ESaveFileName)i;
            Debug.Log(json[i]);
            File.WriteAllText(GetFilePath(FileName), json[i]);
        }        
    }

    public void Load()
    {
        LoadControll();
        LoadSoundInfo();
        LoadUserInfo();
    }

    private void LoadUserInfo()
    {

        CUserInfo tmp = JsonUtility.FromJson<CUserInfo>(File.ReadAllText(GetFilePath(ESaveFileName.UserInfoSaveData)));
        CUserInfo.GetInstance().Money = tmp.Money;
        CUserInfo.GetInstance().CarLv = tmp.CarLv;
        CUserInfo.GetInstance().StoreLv = tmp.StoreLv;

        List<SQuest> Quests = JsonUtility.FromJson<List<SQuest>>(File.ReadAllText(GetFilePath(ESaveFileName.CurrentQuestsSaveData)));
        CUserInfo.GetInstance().QuestLst = Quests;
    }

    private void LoadSoundInfo()
    {        
        string file = File.ReadAllText(GetFilePath(ESaveFileName.SoundSettingSaveData));
        float[] values = new float[2];

        string[] tmp = file.Split(',');

        for(int i = 0; i < tmp.Length; i++)
        {
            values[i] = float.Parse(tmp[i]);
        }

        CSoundManager.GetInstance().BackgroundVolume = values[0];
        CSoundManager.GetInstance().EffectVolume = values[1];
    }

    private void LoadControll()
    {
        CGameInputManager tmp = JsonUtility.FromJson<CGameInputManager>(File.ReadAllText(GetFilePath(ESaveFileName.ControlSettingSaveData)));
        CGameInputManager.GetInstance().MouseReversal = tmp.MouseReversal;
        CGameInputManager.GetInstance().RotationSensitivity = tmp.RotationSensitivity;
    }

    private string GetFilePath(ESaveFileName _ESaveFileName)
    {
        return string.Format("{0}/{1}.txt", FilePath, _ESaveFileName.ToString());
    }
}

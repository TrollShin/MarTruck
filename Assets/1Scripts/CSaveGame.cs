using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public bool LoadUserInfo()
    {
        string UserInfoFilePath = GetFilePath(ESaveFileName.UserInfoSaveData);
        string QuestsFilePath = GetFilePath(ESaveFileName.CurrentQuestsSaveData);

        if (!CheckFilePath(UserInfoFilePath, QuestsFilePath)) return false;

        CUserInfo tmp = JsonUtility.FromJson<CUserInfo>(File.ReadAllText(UserInfoFilePath));
        CUserInfo.GetInstance().Money = tmp.Money;
        CUserInfo.GetInstance().CarLv = tmp.CarLv;
        CUserInfo.GetInstance().StoreLv = tmp.StoreLv;

        List<SQuest> Quests = JsonUtility.FromJson<List<SQuest>>(File.ReadAllText(QuestsFilePath));
        CUserInfo.GetInstance().QuestLst = Quests;

        return true;
    }

    public bool LoadSoundInfo()
    {
        string SoundInfoFilePath = GetFilePath(ESaveFileName.SoundSettingSaveData);

        if(!CheckFilePath(SoundInfoFilePath)) return false;

        string file = File.ReadAllText(SoundInfoFilePath);
        float[] values = new float[2];

        string[] tmp = file.Split(',');

        for(int i = 0; i < tmp.Length; i++)
        {
            values[i] = float.Parse(tmp[i]);
        }

        CSoundManager.GetInstance().BackgroundVolume = values[0];
        CSoundManager.GetInstance().EffectVolume = values[1];

        return true;
    }

    public bool LoadControlSetting()
    {
        string ControlSettingFilePath = GetFilePath(ESaveFileName.ControlSettingSaveData);

        if (!CheckFilePath(ControlSettingFilePath)) return false;

        CGameInputManager tmp = JsonUtility.FromJson<CGameInputManager>(File.ReadAllText(ControlSettingFilePath));
        CGameInputManager.GetInstance().MouseReversal = tmp.MouseReversal;
        CGameInputManager.GetInstance().RotationSensitivity = tmp.RotationSensitivity;

        return true;
    }

    public void ResetData()
    {
        CUserInfo.GetInstance().CarLv = 0;
        CUserInfo.GetInstance().StoreLv = 0;
        CUserInfo.GetInstance().Money = 11;

        Save();
    }

    private string GetFilePath(ESaveFileName _ESaveFileName)
    {
        return string.Format("{0}/{1}.txt", FilePath, _ESaveFileName.ToString());
    }

    private bool CheckFilePath(params string[] _FilePath)
    {
        for(int i = 0; i < _FilePath.Length; i++)
        {
            if (!File.Exists(_FilePath[i])) return false;
        }

        return true;
    }
}

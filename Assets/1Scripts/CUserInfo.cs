using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//필요한거 있으면 추가해 주세요
public class CUserInfo
{
    public int Money;
    public int CarLv;
    public int StoreLv;

    public List<SQuest> QuestLst = new List<SQuest>();

    private static CUserInfo instance;

    public static CUserInfo GetInstance()
    {
        if(instance == null)
        {
            instance = new CUserInfo();
        }

        return instance;
    }
}

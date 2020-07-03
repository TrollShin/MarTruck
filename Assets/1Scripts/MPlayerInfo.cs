using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPlayerInfo : MonoBehaviour
{
    private int money;
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            if (value < 0) money = 0;
            else money = value;
        }
    }
    public List<SQuest> Quests;

    private void Awake()
    {
        Quests = new List<SQuest>();

        Money = CUserInfo.GetInstance().Money;

        InitQuests();
    }

    private void InitQuests()
    {
        if (CUserInfo.GetInstance().QuestsIdx.Length == 0) return;

        List<SQuest> AllQuests = CQuestDBManager.GetInstance().ReadAllQuest();

        for(int i = 0; i < CUserInfo.GetInstance().QuestsIdx.Length; i++)
        {
            Quests.Add(AllQuests[i]);
        }
    }
}

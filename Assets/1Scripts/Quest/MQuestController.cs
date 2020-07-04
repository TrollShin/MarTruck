using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MQuestController
{
    public delegate void OnAddQuest(SQuest Quest);
    public static OnAddQuest AddEvent;

    private float CreateTime = 3f;

    internal List<SQuest> AllQuestList = new List<SQuest>();

    public MQuestController()
    {
        if (AllQuestList.Count != 0) return;

        List<SQuest> AllQuests = CQuestDBManager.GetInstance().ReadAllQuest();

        for (int i = 0; i < AllQuests.Count; i++)
        {
            AllQuestList.Add(AllQuests[i]);
        }
    }

    //Quest를 랜덤으로 생성해서 리스트에 추가해주는 함수.
    public IEnumerator CreateQuestCoroutine()
    {
        while (true)
        {
            SQuest RandomQuest = GetRandomQuest(AllQuestList);

            AddQuest(RandomQuest);

            yield return new WaitForSeconds(CreateTime);
        }
    }

    //List<SQuest> 안에서 랜덤으로 퀘스트를 뽑아주는 함수.
    private SQuest GetRandomQuest(List<SQuest> quests)
    {
        List<SQuest> PossibleQuestList = new List<SQuest>();
        foreach (SQuest temp in quests)
        {
            if (temp.LimitLV <= CUserInfo.GetInstance().StoreLv)
            {
                PossibleQuestList.Add(temp);
            }
        }

        int QuestIndexRandom = Random.Range(0, PossibleQuestList.Count);
        int xPosRandom = Random.Range(0, CUserInfo.GetInstance().StoreLv);
        int yPosRandom = Random.Range(0, CUserInfo.GetInstance().StoreLv);

        SQuest item = new SQuest(PossibleQuestList[QuestIndexRandom]);
        item.TargetPos = new int[2] { xPosRandom, yPosRandom };
        item.IsSuccess = false;
        item.Reward = (xPosRandom + yPosRandom);

        return item;
    }

    //SQuest를 리스트에 추가해주는 함수.
    private void AddQuest(SQuest item)
    {
        CUserInfo.GetInstance().QuestLst.Add(item);
        if (AddEvent != null)
        {
            AddEvent(item);
        }
    }
}

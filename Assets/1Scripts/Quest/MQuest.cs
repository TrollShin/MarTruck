using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MQuest : MonoBehaviour
{
    public delegate void OnLoadQuest(List<SQuest> QuestList);
    public static OnLoadQuest LoadEvent;

    public delegate void OnAddQuest(SQuest Quest);
    public static OnAddQuest AddEvent;

    private float CreateTime = 3f;

    internal List<SQuest> AllQuestList = new List<SQuest>();

    internal List<SQuest> QuestList = new List<SQuest>();

    private void Awake()
    {
        CQuestDBManager.GetInstance().DBCreate();

        AllQuestList = CQuestDBManager.GetInstance().ReadAllQuest();
    }

    //Quest를 랜덤으로 생성해서 리스트에 추가해주는 함수.
    public IEnumerator CreateQuestCoroutine(EStoreLV StoreLV)
    {
        while (true)
        {
            List<SQuest> tempList = new List<SQuest>();
            foreach (SQuest temp in AllQuestList)
            {
                if (temp.LimitLV <= (int)StoreLV)
                {
                    tempList.Add(temp);
                }
            }
            SQuest RandomQuest = GetRandomQuest(tempList);

            AddQuest(RandomQuest);

            yield return new WaitForSeconds(CreateTime);
        }
    }

    //List<SQuest> 안에서 랜덤으로 퀘스트를 뽑아주는 함수.
    private SQuest GetRandomQuest(List<SQuest> quests)
    {
        int random = Random.Range(0, quests.Count);
        SQuest item = new SQuest(quests[random]);
        item.IsSuccess = false;
        item.Reward = Random.Range(10, 51);

        return item;
    }

    //SQuest를 리스트에 추가해주는 함수.
    private void AddQuest(SQuest item)
    {
        QuestList.Add(item);
        if (AddEvent != null)
        {
            AddEvent(item);
        }
    }
}

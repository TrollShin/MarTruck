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
        if (!CQuestDBManager.GetInstance().DBConnectionCheck())
        {
            CQuestDBManager.GetInstance().DBCreate();
        }

        AllQuestList = CQuestDBManager.GetInstance().ReadAllQuest();
    }
    //public IEnumerator AddQuest()
    //{
    //    while (true)
    //    {
    //        SQuest item = CreateQuest();
    //        QuestList.Add(item);
    //        if(AddEvent != null)
    //        {
    //            AddEvent(item);
    //        }

    //        yield return new WaitForSeconds(CreateTime);
    //    }
    //}

    //private SQuest CreateQuest()
    //{
    //    int random = Random.Range(0, AllQuestList.Count);
    //    SQuest item = new SQuest(AllQuestList[random]);
    //    item.IsSuccess = false;
    //    item.Reward = Random.Range(10, 51);

    //    return item;
    //}

    //Quest를 랜덤으로 생성해서 리스트에 추가해주는 함수.
    public IEnumerator CreateQuestCoroutine(EStoreLV StoreLV)
    {
        while (true)
        {
            //List<SQuest> tempList = new List<SQuest>();
            //foreach (SQuest temp in AllQuestList)
            //{
            //    if (temp.limitLV <= StoreLV)
            //    {
            //        tempList.Add(temp);
            //    }
            //}
            List<SQuest> tempList = new List<SQuest>();
            tempList = AllQuestList;
            SQuest RandomQuest = GetRandomQuest(tempList);

            AddQuest(RandomQuest);

            yield return new WaitForSeconds(CreateTime);
        }
    }

    private SQuest GetRandomQuest(List<SQuest> quests)
    {
        Debug.Log(quests.Count + " / " + AllQuestList.Count);
        int random = Random.Range(0, quests.Count);
        SQuest item = new SQuest(quests[random]);
        item.IsSuccess = false;
        item.Reward = Random.Range(10, 51);

        return item;
    }

    private void AddQuest(SQuest item)
    {
        QuestList.Add(item);
        if (AddEvent != null)
        {
            AddEvent(item);
        }
    }
}

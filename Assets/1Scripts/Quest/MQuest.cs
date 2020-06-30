using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MQuest : MonoBehaviour
{
    public delegate void OnLoadQuest(List<SQuest> QuestList);
    public static OnLoadQuest LoadEvent;

    public delegate void OnAddQuest(SQuest Quest);
    public static OnAddQuest AddEvent;

    private float CreateTime = 60f;

    private List<SQuest> AllQuestList = new List<SQuest>();

    private List<SQuest> QuestList = new List<SQuest>();

    private void Awake()
    {
        if(CQuestDBManager.GetInstance().DBConnectionCheck())
        {
            AllQuestList = CQuestDBManager.GetInstance().ReadAllQuest();
        }
        else
        {
            CQuestDBManager.GetInstance().DBCreate();
        }
    }

    private void Start()
    {
        StartCoroutine(AddQuest());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            CSceneFunctionLibrary.ShowQuestMenu(QuestList);
        }
    }

    //Quest 생성을 CreateTime 마다 해주는 코루틴
    IEnumerator AddQuest()
    {
        while (true)
        {
            SQuest item = CreateQuest();
            QuestList.Add(item);
            if(AddEvent != null)
            {
                AddEvent(item);
            }

            yield return new WaitForSeconds(CreateTime);
        }
    }

    //Quest를 랜덤으로 생성해서 리턴해주는 함수.
    private SQuest CreateQuest()
    {
        int random = Random.Range(0, AllQuestList.Count);
        SQuest item = new SQuest(AllQuestList[random]);
        item.IsSuccess = false;
        item.Reward = Random.Range(10, 51);

        return item;
    }
}

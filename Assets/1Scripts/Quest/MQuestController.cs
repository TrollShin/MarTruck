using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MQuestController : MonoBehaviour
{

    private GameObject SelectItem;

    private float CreateTime = 3f;

    private List<SQuest> QuestList = new List<SQuest>();

    public GameObject QuestFrame;

    public GameObject QuestInfo;

    public GameObject Content;

    private void Start()
    {
        ScrollViewInit();
        StartCoroutine(AddQuest());
    }

    //시작 시 스크롤뷰 셋팅해주는 함수.
    private void ScrollViewInit()
    {
        for (int i = 0; i < QuestList.Count; i++)
        {
            AddScrollViewItem(QuestList[i]);
        }
    }

    //스크롤뷰에 quest 추가해주는 함수.
    private void AddScrollViewItem(SQuest quest)
    {
        GameObject index = Instantiate(QuestFrame, new Vector3(0, 0, 0), Quaternion.identity);

        index.transform.GetChild(0).GetComponent<Text>().text = quest.Name;
        index.GetComponent<Button>().onClick.AddListener(ClickQuest);
        index.GetComponent<MStructure>().Quset = quest;
        index.transform.SetParent(Content.transform);
    }

    //Quest 생성을 CreateTime 마다 해주는 코루틴
    IEnumerator AddQuest()
    {
        while (true)
        {
            SQuest item = CreateQuest();
            QuestList.Add(item);
            AddScrollViewItem(item);

            yield return new WaitForSeconds(CreateTime);
        }
    }

    //Quest를 랜덤으로 생성해서 리턴해주는 함수.
    private SQuest CreateQuest()
    {
        CQuestDBManager.GetInstance().DBCreate();

        List<SQuest> temp = CQuestDBManager.GetInstance().ReadAllQuest();
        int random = Random.Range(0, temp.Count);
        SQuest item = new SQuest(temp[random]);
        item.IsSuccess = false;
        item.Reward = Random.Range(10, 51);

        return item;
    }

    //Quest 클릭시 퀘스트 정보를 띄워주는 함수.(UI)
    private void ClickQuest()
    {
        SelectItem = EventSystem.current.currentSelectedGameObject;
        SQuest quest = SelectItem.GetComponent<MStructure>().Quset;

        QuestInfo.transform.GetChild(0).GetComponent<Text>().text = quest.Name;
        QuestInfo.transform.GetChild(1).GetComponent<Text>().text = quest.Description;
        QuestInfo.transform.GetChild(2).GetComponent<Text>().text = quest.Reward.ToString();
    }

    //Quest 수락시 퀘스트 적용시켜주는 함수.
    private void AcceptQuest()
    {
        SQuest myQuest = SelectItem.GetComponent<SQuest>();

    }
}

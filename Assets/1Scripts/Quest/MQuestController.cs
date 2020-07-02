using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MQuestController : MonoBehaviour
{

    private GameObject SelectItem;

    public GameObject QuestFrame;

    public GameObject QuestInfo;

    public GameObject Content;

    private void Awake()
    {
        MQuest.LoadEvent += ScrollViewInit;
        MQuest.AddEvent += AddScrollViewItem;
    }

    //시작 시 스크롤뷰 셋팅해주는 함수.
    private void ScrollViewInit(List<SQuest> QuestList)
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
    public void AcceptQuest()
    {
        SQuest myQuest = SelectItem.GetComponent<SQuest>();

    }
}

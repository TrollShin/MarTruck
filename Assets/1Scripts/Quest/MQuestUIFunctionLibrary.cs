using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class MQuestUIFunctionLibrary : MonoBehaviour
{
    public delegate void OnAddListQuest(SQuest Quest);
    public static OnAddListQuest AddListEvent;


    private GameObject SelectItem;

    public GameObject QuestFrame;

    public GameObject QuestInfo;

    public GameObject Content;

    private void Awake()
    {
        ScrollViewInit();
        MQuestController.AddEvent += AddScrollViewItem;
    }

    //시작 시 스크롤뷰 셋팅해주는 함수.
    private void ScrollViewInit()
    {
        CPlayerState PlayerState = MGameplayStatic.GetPlayerState();
        if (PlayerState.CurrentQuest != null)
        {
            for (int i = 0; i < PlayerState.CurrentQuest.Count; i++)
            {
                AddScrollViewItem(PlayerState.CurrentQuest[i]);
            }
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
        QuestInfo.transform.GetChild(3).GetComponent<Text>().text = quest.TargetPos[0].ToString() + ", " + quest.TargetPos[1].ToString();
    }

    //Quest 수락시 퀘스트 적용시켜주는 함수.
    public void AcceptQuest()
    {
        if (CUserInfo.GetInstance().QuestLst.Count >= 3) return;

        SQuest myQuest = SelectItem.GetComponent<MStructure>().Quset;

        CUserInfo.GetInstance().QuestLst.Add(myQuest);

        AddListEvent(myQuest);

        Destroy(SelectItem);
    }
}

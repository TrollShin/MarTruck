using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MQuestUIFunctionLibrary : MonoBehaviour
{
    public delegate void OnAddListQuest(SQuest Quest);
    public static OnAddListQuest AddListEvent;


    private GameObject SelectItem;

    public GameObject QuestFrame;

    public GameObject QuestInfo;

    public GameObject Content;

    public GameObject MinimapDisplay;

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
        index.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.Name;
        index.GetComponent<Button>().onClick.AddListener(ClickQuest);
        index.GetComponent<MStructure>().Quset = quest;
        index.transform.SetParent(Content.transform);
        index.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
    }

    //Quest 클릭시 퀘스트 정보를 띄워주는 함수.(UI)
    private void ClickQuest()
    {
        SelectItem = EventSystem.current.currentSelectedGameObject;
        SQuest quest = SelectItem.GetComponent<MStructure>().Quset;

        QuestInfo.transform.GetChild(0).GetComponent<Text>().text = quest.Name;
        QuestInfo.transform.GetChild(1).GetComponent<Text>().text = quest.Description;
        QuestInfo.transform.GetChild(2).GetComponent<Text>().text = quest.Reward.ToString();
        QuestInfo.transform.GetChild(3).GetComponent<Text>().text = "(" + quest.TargetPos[0].ToString() + ", " + quest.TargetPos[1].ToString() + ") 블록 " + quest.TargetPos[2] + " 건물";
    }

    //Quest 수락시 퀘스트 적용시켜주는 함수.
    public void AcceptQuest()
    {
        if (CUserInfo.GetInstance().QuestLst.Count >= 3) return;

        if (SelectItem == null) return;

        SQuest myQuest = SelectItem.GetComponent<MStructure>().Quset;

        CUserInfo.GetInstance().QuestLst.Add(myQuest);
        GameObject Structure = GetTargetStructure(myQuest.TargetPos[0], myQuest.TargetPos[1], myQuest.TargetPos[2]);
        Structure.GetComponent<MStructure>().Quset = myQuest;
        MinimapMapping(Structure);

        QuestInfo.transform.GetChild(0).GetComponent<Text>().text = null;
        QuestInfo.transform.GetChild(1).GetComponent<Text>().text = null;
        QuestInfo.transform.GetChild(2).GetComponent<Text>().text = null;
        QuestInfo.transform.GetChild(3).GetComponent<Text>().text = null;

        AddListEvent(myQuest);

        Destroy(SelectItem);
    }

    private void MinimapMapping(GameObject Structure)
    {
        Transform DisplayPos = Structure.transform;

        GameObject display = Instantiate(MinimapDisplay, DisplayPos.position + new Vector3(0, 20, 0), Quaternion.identity);

        display.transform.SetParent(DisplayPos);
    }

    private GameObject GetTargetStructure(int xPos, int yPos, int StructureIndex)
    {
        GameObject Floors = GameObject.Find("StructureFloors");
        GameObject Structure = null;
        for (int i = 0; i < Floors.transform.childCount; i++)
        {
            string name = Floors.transform.GetChild(i).gameObject.name;
            string[] split = name.Split(',');
            string[] splitL = split[0].Split('(');
            string[] splitR = split[1].Split(')');
            if (xPos.ToString().Equals(splitL[1]) && yPos.ToString().Equals(splitR[0]))
            {
                MStructure[] Structures = Floors.transform.GetChild(i).GetComponentsInChildren<MStructure>();
                Structure = Structures[StructureIndex].gameObject;
            }
        }
        return Structure;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public struct SStoreInfo
{
    public List<SQuest> QuestList;
    public EStoreLV StoreLV;

    public SStoreInfo(List<SQuest> QuestList, EStoreLV StoreLV)
    {
        this.QuestList = QuestList.ToArray().Clone() as List<SQuest>;
        this.StoreLV = StoreLV;
    }

    public SStoreInfo(SStoreInfo StoreInfo)
    {
        this.QuestList = StoreInfo.QuestList;
        this.StoreLV = StoreInfo.StoreLV;
    }
}

public enum EStoreLV
{
    MomNPopStore,
    Mart,
    DepartmentStore,
}

public class MStore : MonoBehaviour
{
    private SStoreInfo StoreInfo;

    public GameObject[] Store;

    private GameObject SelectQuest;

    private void Start()
    {
        StoreInit();
    }
    private void StoreInit()
    {
        //if(게임 세이브가 존재할경우)
        //StoreInfo = (gameData)

        //임시
        List<SQuest> q = new List<SQuest>();
        StoreInfo.QuestList = q;
        StoreInfo.StoreLV = EStoreLV.MomNPopStore;

        Store[(int)StoreInfo.StoreLV].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StoreUpgrade();
        }
    }

    private void ShowQuest()
    {
        //StoreInfo.QuestList 를 이용해 Quest 목록 띄우기 - UI 관련
    }

    private void ClickQuest()
    {
        SelectQuest = EventSystem.current.currentSelectedGameObject;
        SQuest quest = SelectQuest.GetComponent<SQuest>();

        //quest 사용하여 정보 띄우기 - UI 관련

    }

    private void AcceptQuest()
    {
        SQuest myQuest = SelectQuest.GetComponent<SQuest>();

        //Quest 적용 - 건물 관련?
    }

    private void StoreUpgrade()
    {
        Store[(int)StoreInfo.StoreLV].SetActive(false);

        StoreInfo.StoreLV += 1;

        Store[(int)StoreInfo.StoreLV].SetActive(true);
    }
}

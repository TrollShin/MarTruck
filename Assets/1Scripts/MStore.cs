using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum EStoreLV
{
    MomNPopStore,
    Mart,
    DepartmentStore,
}

public class MStore : MonoBehaviour
{

    public GameObject[] RoadBlocks;

    public GameObject Floors;

    private MQuestController QuestController;

    private Coroutine CreateQuest;

    public Text EntranceText;

    private void Awake()
    {
        EntranceText = GameObject.Find("Canvas").transform.Find("EntranceText").GetComponent<Text>();
        QuestController = new MQuestController(CQuestDBManager.GetInstance().ReadAllQuest(), Floors);

        MStoreUIFunctionLibrary.StoreEvent = StoreUpgrade;
    }

    private void OnDisable()
    {
        MStoreUIFunctionLibrary.StoreEvent = null;
    }

    private void Start()
    {
        StoreInit();

        CreateQuest = StartCoroutine(QuestController.CreateQuestCoroutine());
    }

    //시작 시 마트 셋팅해주는 함수.
    private void StoreInit()
    {
        for(int i=0; i<RoadBlocks.Length; i++)
        {
            if(i.Equals(CUserInfo.GetInstance().StoreLv))
            {
                RoadBlocks[i].SetActive(true);
            }
            else
            {
                RoadBlocks[i].SetActive(false);
            }
        }
    }

    //마트를 업그레이드 하는 함수.
    private void StoreUpgrade()
    {
        if (CUserInfo.GetInstance().StoreLv >= typeof(EStoreLV).GetEnumValues().Length - 1) return;

        if (CUserInfo.GetInstance().Money >= 15)
        {
            CUserInfo.GetInstance().Money -= 15;

            CUserInfo.GetInstance().StoreLv += 1;

            StopCoroutine(CreateQuest);
            CreateQuest = StartCoroutine(QuestController.CreateQuestCoroutine());

            StoreInit();
        }
    }

    private void CompleteQuest()
    {
        List<SQuest> QuestList = CUserInfo.GetInstance().QuestLst.FindAll((Quest) => { return Quest.IsSuccess; });
        for (int i = 0; i < QuestList.Count; i++)
        {
            CUserInfo.GetInstance().Money += QuestList[i].Reward;
            Destroy(MQuestUIFunctionLibrary.GetTargetStructure(QuestList[i].TargetPos[0], QuestList[i].TargetPos[1], QuestList[i].TargetPos[2]).transform.GetChild(0).gameObject);

            MPopupUI.GetInstance().ShowPopup("퀘스트 완료");
        }
        CUserInfo.GetInstance().QuestLst.RemoveAll((Quest) => { return Quest.IsSuccess; });
        MGameplayStatic.GetPlayerState().QuestSlotList.UpdateQuestList(CUserInfo.GetInstance().QuestLst);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            CompleteQuest();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            EntranceText.gameObject.SetActive(true);
            EntranceText.text = gameObject.name + " 건물에 입장하려면\n" + "V키를 눌러주세요";
            if (Input.GetKeyDown(KeyCode.V))
            {
                Time.timeScale = 0;
                CSceneFunctionLibrary.ShowStoreMenu();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            EntranceText.gameObject.SetActive(false);
        
        }
    }
}

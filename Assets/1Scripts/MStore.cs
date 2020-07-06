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

    //public GameObject[] Store;

    public GameObject Floors;

    private MQuestController QuestController;

    private Coroutine CreateQuest;

    public Text EntranceText;

    private void Awake()
    {
        EntranceText = GameObject.Find("Canvas").transform.Find("EntranceText").GetComponent<Text>();

        CQuestDBManager.GetInstance().DBCreate();
        QuestController = new MQuestController(CQuestDBManager.GetInstance().ReadAllQuest(), Floors);

        CUserInfo.GetInstance().CarLv = 1;
        CUserInfo.GetInstance().StoreLv = 1;
        CUserInfo.GetInstance().Money = 6;
        CUserInfo.GetInstance().QuestLst = new List<SQuest>();

        MStoreUIFunctionLibrary.StoreEvent += StoreUpgrade;
    }

    private void OnDisable()
    {
        MStoreUIFunctionLibrary.StoreEvent -= StoreUpgrade;
    }

    private void Start()
    {
        StoreInit();

        CreateQuest = StartCoroutine(QuestController.CreateQuestCoroutine());
    }

    //시작 시 마트 셋팅해주는 함수.
    private void StoreInit()
    {
        //Store[CUserInfo.GetInstance().StoreLv].SetActive(true);
    }

    //마트를 업그레이드 하는 함수.
    private void StoreUpgrade()
    {
        if (CUserInfo.GetInstance().Money >= 5)
        {
            CUserInfo.GetInstance().Money -= 5;
            //Store[CUserInfo.GetInstance().StoreLv].SetActive(false);

            CUserInfo.GetInstance().StoreLv += 1;

            StopCoroutine(CreateQuest);
            CreateQuest = StartCoroutine(QuestController.CreateQuestCoroutine());

            //Store[CUserInfo.GetInstance().StoreLv].SetActive(true);
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

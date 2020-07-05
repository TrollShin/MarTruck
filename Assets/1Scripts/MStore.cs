using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EStoreLV
{
    MomNPopStore,
    Mart,
    DepartmentStore,
}

public class MStore : MonoBehaviour
{

    public GameObject[] Store;

    public GameObject Floors;

    private MQuestController QuestController;

    private Coroutine CreateQuest;

    private void Awake()
    {
        CQuestDBManager.GetInstance().DBCreate();
        QuestController = new MQuestController(CQuestDBManager.GetInstance().ReadAllQuest(), Floors);

        CUserInfo.GetInstance().CarLv = 0;
        CUserInfo.GetInstance().StoreLv = 0;
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
        Store[CUserInfo.GetInstance().StoreLv].SetActive(true);
    }

    //마트를 업그레이드 하는 함수.
    private void StoreUpgrade()
    {
        Store[CUserInfo.GetInstance().StoreLv].SetActive(false);

        CUserInfo.GetInstance().StoreLv += 1;

        StopCoroutine(CreateQuest);
        CreateQuest = StartCoroutine(QuestController.CreateQuestCoroutine());

        Store[CUserInfo.GetInstance().StoreLv].SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            CSceneFunctionLibrary.ShowStoreMenu();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            CSceneFunctionLibrary.ShowRepairMenu();
        }
    }
}

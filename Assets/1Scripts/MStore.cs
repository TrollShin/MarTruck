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

    private MQuestController QuestController = new MQuestController();

    private Coroutine CreateQuest;

    private void Awake()
    {
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
        if (CUserInfo.GetInstance() != null)
        {
            Store[CUserInfo.GetInstance().StoreLv].SetActive(true);
        }
    }

    //마트를 업그레이드 하는 함수.
    private void StoreUpgrade()
    {
        if(CUserInfo.GetInstance() != null)
        {
            Store[CUserInfo.GetInstance().StoreLv].SetActive(false);

            CUserInfo.GetInstance().StoreLv += 1;

            StopCoroutine(CreateQuest);
            CreateQuest = StartCoroutine(QuestController.CreateQuestCoroutine());

            Store[CUserInfo.GetInstance().StoreLv].SetActive(true);
        }
        
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

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
    private EStoreLV StoreLV;

    public GameObject[] Store;

    private MQuest QuestController;

    private Coroutine CreateQuest;

    private void Start()
    {
        StoreInit();

        QuestController = GetComponent<MQuest>();

        CreateQuest = StartCoroutine(QuestController.CreateQuestCoroutine(StoreLV));
    }

    //시작 시 마트 셋팅해주는 함수.
    private void StoreInit()
    {
        //if(게임 세이브가 존재할경우)
        //StoreInfo = (gameData)

        StoreLV = EStoreLV.MomNPopStore;

        Store[(int)StoreLV].SetActive(true);
    }

    //마트를 업그레이드 하는 함수.
    private void StoreUpgrade()
    {
        Store[(int)StoreLV].SetActive(false);

        StoreLV += 1;

        StopCoroutine(CreateQuest);
        CreateQuest = StartCoroutine(QuestController.CreateQuestCoroutine(StoreLV));

        Store[(int)StoreLV].SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            CSceneFunctionLibrary.ShowQuestMenu(QuestController.QuestList);
        }
    }
}

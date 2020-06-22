using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SStoreInfo
{
    List<SQuest> QuestList;
    EStoreLV StoreLV;

    SStoreInfo(List<SQuest> QuestList, EStoreLV StoreLV)
    {
        this.QuestList = QuestList.ToArray().Clone() as List<SQuest>;
        this.StoreLV = StoreLV;
    }

    SStoreInfo(SStoreInfo StoreInfo)
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
<<<<<<< HEAD
    private SStoreInfo StoreInfo;

    public GameObject[] Car;
    public GameObject[] Store;

    public MDriftCamera driftCamera;
    private void Start()
    {
        GameInit();
    }
    private void GameInit()
    {
        //if(게임 세이브가 존재할경우)
        //StoreInfo = (gameData)

        //임시
        List<SQuest> q = new List<SQuest>();
        StoreInfo.QuestList = q;
        StoreInfo.StoreLV = EStoreLV.MomNPopStore;

        GameObject curCar = Car[(int)StoreInfo.StoreLV];

        curCar.SetActive(true);

        SetCamera(curCar);

        Store[(int)StoreInfo.StoreLV].SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            StoreUpgrade();
        }
    }

    private void SetCamera(GameObject Car)
    {
        Transform CamRig = Car.transform.Find("CamRig");

        driftCamera.lookAtTarget = CamRig.Find("CamLookAtTarget");
        driftCamera.positionTarget = CamRig.Find("CamPosition");
        driftCamera.basicPosTarget = CamRig.Find("CamBasic");
=======
    // Start is called before the first frame update
    void Start()
    {
        
>>>>>>> 70b9a2fc644457c6ee6d8ae555b77984af815c67
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

﻿using System.Collections;
using System.Collections.Generic;
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

    private void SetCamera(GameObject Car)
    {
        Transform CamRig = Car.transform.Find("CamRig");

        driftCamera.lookAtTarget = CamRig.Find("CamLookAtTarget");
        driftCamera.positionTarget = CamRig.Find("CamPosition");
        driftCamera.sideView = CamRig.Find("CamSidePosition");
    }

    private void StoreUpgrade()
    {
        Car[(int)StoreInfo.StoreLV].SetActive(false);
        Store[(int)StoreInfo.StoreLV].SetActive(false);

        StoreInfo.StoreLV += 1;

        Car[(int)StoreInfo.StoreLV].SetActive(true);
        Store[(int)StoreInfo.StoreLV].SetActive(true);

        SetCamera(Car[(int)StoreInfo.StoreLV]);
    }
}

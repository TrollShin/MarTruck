using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECarLV
{
    Porter,
    Porter_Container,
    Truck,
}

public class MRepairShop : MonoBehaviour
{
    public GameObject[] Car;
    public ECarLV CarLV;

    public MDriftCamera driftCamera;
    void Start()
    {
        CarInit();
    }

    private void CarInit()
    {
        CarLV = ECarLV.Porter;

        GameObject curCar = Car[(int)CarLV];

        curCar.SetActive(true);

        SetCamera(curCar);
    }
    private void StoreUpgrade()
    {
        Car[(int)CarLV].SetActive(false);

        CarLV += 1;

        Car[(int)CarLV].SetActive(true);

        SetCamera(Car[(int)CarLV]);
    }

    private void SetCamera(GameObject Car)
    {
        Transform CamRig = Car.transform.Find("CamRig");

        driftCamera.lookAtTarget = CamRig.Find("CamLookAtTarget");
        driftCamera.positionTarget = CamRig.Find("CamPosition");
        driftCamera.basicPosTarget = CamRig.Find("CamBasic");
    }
}

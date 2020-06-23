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
    ECarLV CarLV;

    public MDriftCamera driftCamera;
    void Start()
    {
        CarInit();
    }

    private void CarInit()
    {
        CarLV = ECarLV.Porter;

        Car[(int)CarLV].SetActive(true);

        SetCamera(Car[(int)CarLV]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CarUpgrade();
        }
    }

    private void CarUpgrade()
    {
        Car[(int)CarLV].SetActive(false);

        CarLV += 1;

        Car[(int)CarLV].SetActive(true);

        SetCamera(Car[(int)CarLV]);
    }

    private void SetCamera(GameObject Car)
    {
        Transform CamRig = Car.transform.Find("CamRig");

        driftCamera.CamAxis = CamRig;
        driftCamera.positionTarget = CamRig.GetChild(0);
    }
}

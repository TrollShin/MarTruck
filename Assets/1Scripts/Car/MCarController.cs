using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECarLV
{
    Porter,
    Porter_Container,
    Truck,
}

public class MCarController : MonoBehaviour
{
    private ECarLV CarLV;

    private MDriftCamera driftCamera;

    private List<GameObject> Cars = new List<GameObject>();

    void Start()
    {
        driftCamera = GetComponent<MDriftCamera>();

        CarInit();
        
    }

    private void Awake()
    {
        MRepairShopUIFunctionLibrary.UpgradeEvent += UpgradeCar;
    }

    private void OnDisable()
    {
        MRepairShopUIFunctionLibrary.UpgradeEvent -= UpgradeCar;
    }

    //Car를 초기화해주는 함수.
    private void CarInit()
    {
        CarLV = ECarLV.Porter;

        for (int i = 0; i < transform.childCount ; i++)
        {
            Cars.Add(transform.GetChild(i).gameObject);
        }

        CarSetting(CarLV);
    }

    //ECarLV에 맞춰 Car를 설정해주는 함수.
    private void CarSetting(ECarLV lv)
    {
        for (int i = 0; i < Cars.Count; i++)
        {
            if (i.Equals((int)lv))
            {
                Cars[(int)lv].SetActive(true);
            }
            else
            {
                Cars[i].SetActive(false);
            }
        }

        SetCamera(Cars[(int)CarLV]);
    }

    //Car를 업그레이드 하는 함수.
    private void UpgradeCar()
    {
        CarLV++;
        CarSetting(CarLV);
    }

    //차를 받아 카메라 셋팅해주는 함수.
    private void SetCamera(GameObject Car)
    {
        Transform CamRig = Car.transform.Find("CamRig");

        driftCamera.CamAxis = CamRig;
        driftCamera.positionTarget = CamRig.GetChild(0);
    }
}

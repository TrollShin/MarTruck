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
        for (int i = 0; i < transform.childCount ; i++)
        {
            Cars.Add(transform.GetChild(i).gameObject);
        }

        if (CUserInfo.GetInstance() != null)
        {
            CarSetting((ECarLV)CUserInfo.GetInstance().CarLv);
        }
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

        if (CUserInfo.GetInstance() != null)
        {
            SetCamera(Cars[CUserInfo.GetInstance().CarLv]);
        }
    }

    //Car를 업그레이드 하는 함수.
    private void UpgradeCar()
    {
        if (CUserInfo.GetInstance() != null)
        {
            CUserInfo.GetInstance().CarLv++;
            CarSetting((ECarLV)CUserInfo.GetInstance().CarLv);
        }
    }

    //차를 받아 카메라 셋팅해주는 함수.
    private void SetCamera(GameObject Car)
    {
        Transform CamRig = Car.transform.Find("CamRig");

        driftCamera.CamAxis = CamRig;
        driftCamera.positionTarget = CamRig.GetChild(0);
    }
}

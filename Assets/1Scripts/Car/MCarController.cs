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

    private void Awake()
    {
        driftCamera = GetComponent<MDriftCamera>();

        MRepairShopUIFunctionLibrary.UpgradeEvent += NextCarSetting;
    }

    private void Start()
    {
        CarInit();
    }

    private void OnDisable()
    {
        MRepairShopUIFunctionLibrary.UpgradeEvent -= NextCarSetting;
    }

    //Car를 초기화해주는 함수.
    private void CarInit()
    {
        for (int i = 0; i < transform.childCount ; i++)
        {
            Cars.Add(transform.GetChild(i).gameObject);
        }
        CarSetting();
    }

    //CarLV에 맞춰 Car를 설정해주는 함수.
    private void CarSetting()
    {
        if(MGameplayStatic.GetPlayerState() == null)
        {
            Debug.LogError("MGameplayStatic 인스턴스를 생성해주세요.");
            return;
        }

        for (int i = 0; i < Cars.Count; i++)
        {
            if (i.Equals(CUserInfo.GetInstance().CarLv))
            {
                Cars[i].SetActive(true);

                MGameplayStatic.GetPlayerState().CurrentCar = Cars[i].GetComponent<MCar>();
                SetCamera(Cars[i]);
            }
            else
            {
                Cars[i].SetActive(false);
            }
        }
    }

    //Car를 업그레이드 하는 함수.
    private void NextCarSetting()
    {
        CUserInfo.GetInstance().CarLv++;
        Cars[CUserInfo.GetInstance().CarLv].transform.position = Cars[CUserInfo.GetInstance().CarLv - 1].transform.position;
        Cars[CUserInfo.GetInstance().CarLv].transform.rotation = Cars[CUserInfo.GetInstance().CarLv - 1].transform.rotation;
        CarSetting();
    }

    //차를 받아 카메라 셋팅해주는 함수.
    private void SetCamera(GameObject Car)
    {
        Transform CamRig = Car.transform.Find("CamRig");

        driftCamera.CamAxis = CamRig;
        driftCamera.positionTarget = CamRig.GetChild(0);
    }
}

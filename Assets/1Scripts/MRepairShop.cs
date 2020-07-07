using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MRepairShop : MonoBehaviour
{
    public delegate void OnChangeCar();
    public static event OnChangeCar RepairEvent;

    public Text EntranceText;

    private void Awake()
    {
        EntranceText = GameObject.Find("Canvas").transform.Find("EntranceText").GetComponent<Text>();


        MRepairShopUIFunctionLibrary.RefuelEvent += Refueling;
    }

    private void OnDisable()
    {
        MRepairShopUIFunctionLibrary.RefuelEvent -= Refueling;
    }

    //차를 업그레이드 해주는 함수.
    private void CarUpgrade()
    {
        if(CUserInfo.GetInstance().Money >= 5)
        {
            CUserInfo.GetInstance().Money -= 5;
            RepairEvent();
            MGameplayStatic.GetPlayerState().CurrentCar.GetComponentInChildren<ParticleSystem>().Play();
        }
    }

    //차의 연료를 채워주는 함수.
    private void Refueling()
    {
        CPlayerState PlayerState = MGameplayStatic.GetPlayerState();
        if(PlayerState != null)
        {
            PlayerState.CurrentCar.CarInfo.Fuel = PlayerState.CurrentCar.CarInfo.MaxFuel;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            EntranceText.gameObject.SetActive(true);
            EntranceText.text = gameObject.name + " 건물에 입장하려면\n" + "V키를 눌러주세요";
            if (Input.GetKeyDown(KeyCode.V))
            {
                Time.timeScale = 0;
                CSceneFunctionLibrary.ShowRepairMenu();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            EntranceText.gameObject.SetActive(false);

        }
    }
}

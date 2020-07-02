using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRepairShop : MonoBehaviour
{
    public delegate void OnChangeCar();
    public static event OnChangeCar RepairEvent;

    private MCar Car;

    private void Awake()
    {
        MRepairShopUIFunctionLibrary.RefuelEvent += Refueling;
    }

    private void OnDisable()
    {
        MRepairShopUIFunctionLibrary.RefuelEvent -= Refueling;
    }

    //차를 업그레이드 해주는 함수.
    private void CarUpgrade()
    {
        RepairEvent();
    }

    //차의 연료를 채워주는 함수.
    private void Refueling()
    {
        if(Car != null)
        {
            Car.CarInfo.Fuel = Car.CarInfo.MaxFuel;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Car"))
        {
            Car = collision.GetComponent<MCar>();
        }
    }
}

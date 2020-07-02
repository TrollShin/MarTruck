using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRepairShop : MonoBehaviour
{
    public delegate void OnChangeCar();
    public static event OnChangeCar EventUpgradeCar;

    private void CarUpgrade()
    {
        EventUpgradeCar();
    }

    private void Refueling(MCar car)
    {
        car.CarInfo.Fuel = car.CarInfo.MaxFuel;
    }
}

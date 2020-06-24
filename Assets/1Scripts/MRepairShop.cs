using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MRepairShop : MonoBehaviour
{
    public delegate void OnChangeCar();
    public static event OnChangeCar EventUpgradeCar;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CarUpgrade();
        }
    }

    private void CarUpgrade()
    {
        EventUpgradeCar();
    }

    private void Refueling()
    {

    }
}

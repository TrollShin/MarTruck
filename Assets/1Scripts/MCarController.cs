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
        driftCamera = Camera.main.GetComponent<MDriftCamera>();

        CarInit();
        MRepairShop.EventUpgradeCar += UpgradeCar;
    }

    private void CarInit()
    {
        CarLV = ECarLV.Porter;

        for (int i = 0; i < transform.childCount ; i++)
        {
            Cars.Add(transform.GetChild(i).gameObject);
        }

        SetCar(CarLV);
    }

    private void SetCar(ECarLV lv)
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

    private void UpgradeCar()
    {
        CarLV++;
        SetCar(CarLV);
    }

    private void SetCamera(GameObject Car)
    {
        Transform CamRig = Car.transform.Find("CamRig");

        driftCamera.CamAxis = CamRig;
        driftCamera.positionTarget = CamRig.GetChild(0);
    }
}

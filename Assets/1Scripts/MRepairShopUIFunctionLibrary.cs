using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MRepairShopUIFunctionLibrary : MonoBehaviour
{

    public delegate void OnUpgradeCar();
    public static OnUpgradeCar UpgradeEvent;

    public delegate void OnRefuelCar();
    public static OnRefuelCar RefuelEvent;

    public void OnClickExit()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName("StoreUI"));
    }

    public void OnUpgrade()
    {
        UpgradeEvent();
    }

    public void OnRefueling()
    {
        RefuelEvent();
    }
}

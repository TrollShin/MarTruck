using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MDisplayFuel : MonoBehaviour
{
    public float FuelRatio
    {
        set
        {
            FuelImage.fillAmount = value;
        }
    }

    [SerializeField]
    private Image FuelImage;

    private void Update()
    {
        CPlayerState PlayerState = MGameplayStatic.GetPlayerState();
        if (PlayerState == null)
            return;

        SCarInfo CarInfo = PlayerState.CurrentCar;
        FuelRatio = CarInfo.Fuel / CarInfo.MaxFuel;
    }
}

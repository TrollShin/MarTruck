using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ETrafficLightState
{
    Red = 0,
    Green = 1,
    Yellow = 2,
    None = 3,
}

public class MTrafficLightSignal : MonoBehaviour
{
    public delegate void TrafficLightEvent(ETrafficLightState _Signal);
    public event TrafficLightEvent OnSignalChanged;

    [SerializeField]
    private float FloatTrafficLightChangeDelayTime;

    private WaitForSeconds TrafficLightChangeDelayTime;

    private ETrafficLightState Signal = 0;

    private void Start()
    {
        TrafficLightChangeDelayTime = new WaitForSeconds(FloatTrafficLightChangeDelayTime);

        StartCoroutine(CChangeTrafficLightSignal());
    }

    IEnumerator CChangeTrafficLightSignal()
    {
        if (Signal == ETrafficLightState.Yellow)
        {
            Signal = 0;
        }
        else
        {
            Signal += 1;
        }

        Debug.Log("In TrafficLight" + Signal);
        OnSignalChanged?.Invoke(Signal);

        yield return TrafficLightChangeDelayTime;

        StartCoroutine(CChangeTrafficLightSignal());
    }

    public ETrafficLightState GetSignal()
    {
        return Signal;        
    }
}

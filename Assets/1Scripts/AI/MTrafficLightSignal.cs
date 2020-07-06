using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//끝은 None
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
        OnSignalChanged?.Invoke(Signal);

        Signal += 1;

        yield return TrafficLightChangeDelayTime;

        //None 즉 ETrafficLightState의 끝에오면 처음으로 바꿈
        if (Signal == ETrafficLightState.None)
        {
            Signal = 0;
        }

        StartCoroutine(CChangeTrafficLightSignal());
    }

    public ETrafficLightState GetSignal()
    {
        return Signal;        
    }
}

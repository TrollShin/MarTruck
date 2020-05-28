using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SCarInfo
{
    float MaxSpeed;
    float Acceleration;
    float Friction;
    float Braking;

    SCarInfo(float MaxSpeed, float Acceleration, float Friction, float Braking)
    {
        this.MaxSpeed = MaxSpeed;
        this.Acceleration = Acceleration;
        this.Friction = Friction;
        this.Braking = Braking;
    }

    SCarInfo(SCarInfo CarInfo)
    {
        this.MaxSpeed = CarInfo.MaxSpeed;
        this.Acceleration = CarInfo.Acceleration;
        this.Friction = CarInfo.Friction;
        this.Braking = CarInfo.Braking;
    }
}

public class MCar : MonoBehaviour
{

}

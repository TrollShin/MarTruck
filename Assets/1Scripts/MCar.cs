using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SCarInfo
{
    public float MaxSpeed;
    public float Acceleration;
    public float Friction;
    public float Braking;
    public float MaxAngle;

    SCarInfo(float MaxSpeed, float Acceleration, float Friction, float Braking, float MaxAngle)
    {
        this.MaxSpeed = MaxSpeed;
        this.Acceleration = Acceleration;
        this.Friction = Friction;
        this.Braking = Braking;
        this.MaxAngle = MaxAngle;
    }

    SCarInfo(SCarInfo CarInfo)
    {
        this.MaxSpeed = CarInfo.MaxSpeed;
        this.Acceleration = CarInfo.Acceleration;
        this.Friction = CarInfo.Friction;
        this.Braking = CarInfo.Braking;
        this.MaxAngle = CarInfo.MaxAngle;
    }
}

public class MCar : MonoBehaviour
{
    public SCarInfo CarInfo;

    public GameObject WheelShape;

    private WheelCollider[] ColliderWheels;

    void Start()
    {
        ColliderWheels = GetComponentsInChildren<WheelCollider>();

        for (int i = 0; i < ColliderWheels.Length; ++i)
        {
            var wheel = ColliderWheels[i];

            if (WheelShape != null)
            {
                var ws = Instantiate(WheelShape);
                ws.transform.parent = wheel.transform;
            }
        }
    }

    private void FixedUpdate()
    {
        float Angle = CarInfo.MaxAngle * Input.GetAxis("Horizontal");
        float Torque = CarInfo.Acceleration * Input.GetAxis("Vertical");

        float HandBrake = Input.GetKey(KeyCode.Space) ? CarInfo.Braking : 0;

        foreach (WheelCollider Wheel in ColliderWheels)
        {
            if (Wheel.transform.localPosition.z > 0)
                Wheel.steerAngle = Angle;

            if (Wheel.transform.localPosition.z < 0)
            {
                Wheel.brakeTorque = HandBrake;

                if (GetComponent<Rigidbody>().velocity.magnitude * 3.6 > CarInfo.MaxAngle)
                {
                    Vector3 VelocityUnit = GetComponent<Rigidbody>().velocity.normalized; // normalized 로 단위 벡터(1의 길이를 가진 벡터)를 받고
                    GetComponent<Rigidbody>().velocity = VelocityUnit * CarInfo.MaxAngle / 3.6f; // 단위 벡터에 최고속도만큼의 값을 곱해서 rigidbody 의 velocity를 고정
                }
                else
                {
                    Wheel.motorTorque = Torque;
                }
            }

            if (WheelShape)
            {
                Quaternion q;
                Vector3 p;
                Wheel.GetWorldPose(out p, out q);

                Transform ShapeTransform = Wheel.transform.GetChild(0);

                if (Wheel.tag.Contains("RightTire"))
                {
                    ShapeTransform.rotation = q * Quaternion.Euler(0, 0, 90);
                    ShapeTransform.position = p;
                }
                else
                {
                    ShapeTransform.position = p;
                    ShapeTransform.rotation = q * Quaternion.Euler(0, 180, 90); ;
                }
            }
        }
    }
}

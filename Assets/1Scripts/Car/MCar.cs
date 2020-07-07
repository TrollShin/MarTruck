using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

[System.Serializable]
public struct SCarInfo
{
    public float MaxFuel;
    public float MaxSpeed;
    public float Acceleration;
    public float Friction;
    public float Braking;
    public float MaxAngle;

    public float Fuel;

    SCarInfo(float MaxFuel, float MaxSpeed, float Acceleration, float Friction, float Braking, float MaxAngle, float Fuel)
    {
        this.MaxFuel = MaxFuel;
        this.MaxSpeed = MaxSpeed;
        this.Acceleration = Acceleration;
        this.Friction = Friction;
        this.Braking = Braking;
        this.MaxAngle = MaxAngle;
        this.Fuel = Fuel;
    }

    SCarInfo(SCarInfo CarInfo)
    {
        /*
        Type type = typeof(SCarInfo);

        foreach(FieldInfo field in type.GetFields())
        {
            field.SetValue(this, field.GetValue(CarInfo));
        }
        */

        this.MaxSpeed = CarInfo.MaxSpeed;
        this.Acceleration = CarInfo.Acceleration;
        this.Friction = CarInfo.Friction;
        this.Braking = CarInfo.Braking;
        this.MaxAngle = CarInfo.MaxAngle;
        this.MaxFuel = CarInfo.MaxFuel;
        this.Fuel = CarInfo.Fuel;
    }
}

public class MCar : MonoBehaviour
{
    public SCarInfo CarInfo;

    public GameObject WheelShape;

    private WheelCollider[] ColliderWheels;

    private float Angle;
    private float Torque;
    private float HandBrake;

    private AudioSource audioSource;

    private void Awake()
    {
        CarInfo.Fuel = CarInfo.MaxFuel;
        audioSource = GetComponent<AudioSource>();
    }

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

    //차의 Torque를 리턴하는 함수.
    private float GetCarTorque()
    {
        return CarInfo.Fuel > 0 ? CarInfo.Acceleration * Input.GetAxis("Vertical") : 0;
    }
    private void EngineSound()
    {
        audioSource.pitch = (GetComponent<Rigidbody>().velocity.magnitude * 3.6f / CarInfo.MaxSpeed + 1) * CSoundManager.GetInstance().EffectVolume; 
    }

    //Wheel에 Tire 모델을 맞춰주는 함수.
    private void SetWheelShape(WheelCollider Wheel)
    {
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

    private void FixedUpdate()
    {
        Angle = CarInfo.MaxAngle * Input.GetAxis("Horizontal");

        Torque = GetCarTorque();

        HandBrake = Input.GetKey(KeyCode.Space) ? CarInfo.Braking : 0;

        EngineSound();

        foreach (WheelCollider Wheel in ColliderWheels)
        {
            if (Wheel.transform.localPosition.z > 0)
                Wheel.steerAngle = Angle;

            if (Wheel.transform.localPosition.z < 0)
            {
                Wheel.brakeTorque = HandBrake;

                if (GetComponent<Rigidbody>().velocity.magnitude * 3.6f > CarInfo.MaxSpeed)
                {
                    Vector3 VelocityUnit = GetComponent<Rigidbody>().velocity.normalized; // normalized 로 단위 벡터(1의 길이를 가진 벡터)를 받고
                    GetComponent<Rigidbody>().velocity = VelocityUnit * CarInfo.MaxSpeed / 3.6f; // 단위 벡터에 최고속도만큼의 값을 곱해서 rigidbody 의 velocity를 고정
                }
                else
                {
                    Wheel.motorTorque = Torque;
                }
                CarInfo.Fuel -= Math.Abs(Torque) * 0.001f;
            }

            SetWheelShape(Wheel);
        }
    }

    private void Update()
    {
        Camera cam = transform.GetComponentInChildren<Camera>();
        cam.transform.position = transform.position + new Vector3(0, 100, 0);
        cam.transform.LookAt(transform);

        GameObject icon = cam.transform.parent.GetChild(1).gameObject;
        icon.transform.position = transform.position + new Vector3(0, 6, 0);
    }
}

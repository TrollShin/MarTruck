using System;
using UnityEngine;

public class MDriftCamera : MonoBehaviour
{
    [Serializable]
    public class AdvancedOptions
    {
        public bool updateCameraInUpdate;
        public bool updateCameraInFixedUpdate = true;
        public bool updateCameraInLateUpdate;
    }

    public float smoothing = 6f;

    public Transform positionTarget;
    public Transform CamAxis;

    public AdvancedOptions advancedOptions;

    private float RotationSpeed = 6f;

    private Vector3 Gap;               // 회전 축적 값.

    private void FixedUpdate()
    {
        if (advancedOptions.updateCameraInFixedUpdate)
            UpdateCamera();
    }

    private void Update()
    {
        if (advancedOptions.updateCameraInUpdate)
            UpdateCamera();
    }

    private void LateUpdate()
    {
        if (advancedOptions.updateCameraInLateUpdate)
            UpdateCamera();
    }


    private void UpdateCamera()
    {
        transform.position = Vector3.Lerp(transform.position, positionTarget.position, smoothing * Time.deltaTime); //positionTarget.position;
        transform.LookAt(CamAxis);

        // 값을 축적.
        Gap.x += Input.GetAxis("Mouse Y") * RotationSpeed * -1;
        Gap.y += Input.GetAxis("Mouse X") * RotationSpeed;

        // 카메라 회전범위 제한.
        Gap.x = Mathf.Clamp(Gap.x, -5f, 70f);

        CamAxis.transform.localEulerAngles = Gap;
    }
}

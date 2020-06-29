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

    private Vector3 Gap;               // ȸ�� ���� ��.

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
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, positionTarget.position, smoothing * Time.deltaTime); //positionTarget.position;
        Camera.main.transform.LookAt(CamAxis);

        // ���� ����.
        Gap.x += Input.GetAxis("Mouse Y") * CGameInputManager.GetInstance().RotationSensitivity * -1;
        Gap.y += Input.GetAxis("Mouse X") * CGameInputManager.GetInstance().RotationSensitivity * (int)CGameInputManager.GetInstance().MouseReversal;

        // ī�޶� ȸ������ ����.
        Gap.x = Mathf.Clamp(Gap.x, -5f, 70f);

        CamAxis.transform.localEulerAngles = Gap;
    }
}

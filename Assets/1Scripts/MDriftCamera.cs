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
    public Transform lookAtTarget;
    public Transform positionTarget;
    public Transform basicPosTarget;
    public AdvancedOptions advancedOptions;

    private float RotationSpeed = 0.1f;
    private Vector3 beforePos;

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
        if (Input.GetMouseButton(1))
        {
            if (beforePos == Vector3.zero)
            {
                beforePos = Input.mousePosition;
            }

            if (beforePos != Vector3.zero)
            {
                positionTarget.transform.RotateAround(positionTarget.parent.position, Vector3.down, (beforePos.x - Input.mousePosition.x) * Time.deltaTime * RotationSpeed);
            }

            transform.position = positionTarget.position; //Vector3.Lerp(transform.position, positionTarget.position, Time.deltaTime * smoothing);
            transform.LookAt(positionTarget.parent);
        }
        else
        {
            beforePos = Vector3.zero;

            transform.position = Vector3.Lerp(transform.position, basicPosTarget.position, Time.deltaTime * smoothing);
            positionTarget.position = basicPosTarget.position;

            transform.LookAt(lookAtTarget);
        }
    }
}

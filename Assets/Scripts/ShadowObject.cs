using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowObject : MonoBehaviour
{

    [SerializeField] private Transform shadowOnly;
    [SerializeField] private Transform meshOnly;
    [SerializeField] private Transform correctFormat;
    [SerializeField] private float rotationTolerance;
    [SerializeField] private float positionTolerance;
    [SerializeField] private bool canMoveHorizontally;
    [SerializeField] private bool canMoveVertically;
    [SerializeField] private bool canRotateHorizontally;
    [SerializeField] private bool canRotateVertically;

    private Vector3 _initForward;

    private void Start()
    {
        _initForward = correctFormat.forward;
    }

    public void Move(Vector3 movingDelta)
    {
        shadowOnly.Translate(movingDelta,Space.World);
        meshOnly.Translate(movingDelta,Space.World);
        correctFormat.Translate(movingDelta,Space.World);
    }

    public void Rotate(Vector3 rotationDir,float rotationSpeed)
    {
        Vector3 rotHorizontal = canRotateHorizontally ? Vector3.up * -rotationDir.x * rotationSpeed : Vector3.zero;
        Vector3 rotVertical = canRotateVertically ? Vector3.right * rotationDir.y * rotationSpeed : Vector3.zero;
        Vector3 rotationDelta = rotHorizontal + rotVertical;
        shadowOnly.Rotate(rotationDelta,Space.World);
        meshOnly.Rotate(rotationDelta,Space.World);
    }
    
    public bool CheckIfRotationCorrect()
    {
        Quaternion targetLocalRotation = shadowOnly.rotation;
        Quaternion correctRot = correctFormat.rotation;
        Quaternion checkRotation = Quaternion.Euler(targetLocalRotation.eulerAngles.x,
            targetLocalRotation.eulerAngles.y,
            correctRot.eulerAngles.z);
        bool isRotationCloseEnough = Quaternion.Angle(checkRotation, correctRot) <= rotationTolerance;
        return isRotationCloseEnough;
    }

    public IEnumerator LerpToRotation()
    {
        float angle = Quaternion.Angle(correctFormat.rotation, shadowOnly.rotation);
        float deltaTimeSpeed = Time.deltaTime / angle;
        float time = 0f;
        while (time < 1f)
        {
            Quaternion q = Quaternion.Lerp(shadowOnly.rotation, correctFormat.rotation, time);
            meshOnly.rotation = q;
            shadowOnly.rotation = q;
            time += deltaTimeSpeed;
            yield return null;
        }
    }
    public bool IsPositionCorrect()
    {
        return true;
    }
}

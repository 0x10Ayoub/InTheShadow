using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Drawing;
using System.Text;

public class Rotation : MonoBehaviour
{
    public Transform shadowOnly;
    public Transform meshOnly;
    public Transform correctFormat;
    public ShadowPiece sp;
    private Vector3 _mouseControl;
    private Vector3 _firstPos;
    private Vector3 _newMousePos;
    private float _rotationSpeed;
    private float _movingSpeed;
    void Start()
    {
        _mouseControl = Vector3.zero;
        _movingSpeed = 25 * Time.deltaTime;
        _rotationSpeed = (400 * Time.deltaTime);
    }
    void Update()
    {
        // correctFormat.Rotate(Vector3.forward * Time.deltaTime * 10,Space.World);
        // Debug.Log($"{shadowOnly.localRotation.eulerAngles}");
        MoveAndRotate();
    }

    private void MoveAndRotate()
    {
        if(!Input.GetMouseButton(0)) return;
        var x =  Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");
        if (Input.GetKey(KeyCode.LeftControl) && sp.canMove)
        {
            Vector3 movingDelta = Vector3.up * y * _movingSpeed + Vector3.right * x * _movingSpeed;
            shadowOnly.Translate(movingDelta,Space.World);
            meshOnly.Translate(movingDelta,Space.World);
        }
        else
        {
            Vector3 rotHorizontal = sp.canRotateHorizontal ? Vector3.up * -x * _rotationSpeed : Vector3.zero;
            Vector3 rotVertical = sp.canRotateVertical ? Vector3.right * y * _rotationSpeed : Vector3.zero;
            Vector3 rotationDelta = rotHorizontal + rotVertical;
            shadowOnly.Rotate(rotationDelta,Space.World);
            meshOnly.Rotate(rotationDelta,Space.World);
            CheckRotationDiff();
        }
    }


    void CheckRotationDiff()
    {
       checkRotaionisCorrect(shadowOnly);
        //CheckIfRotationCorrect(shadowOnly);
        //Debug.Log($"{meshOnly.forward} {correctFormat.forward} {Vector3.Dot(meshOnly.forward,correctFormat.forward)}");
        //Debug.Log(Quaternion.Angle(meshOnly.rotation,correctFormat.rotation));
    }

    public bool CheckIfRotationCorrect(Transform target)
    {
        Quaternion targetLocalRotation = target.localRotation;
        for (int i = 0; i <= 360; i++)
        {
            Debug.Log($"{Quaternion.Angle(target.localRotation, correctFormat.localRotation)}");
            if (Quaternion.Angle(target.localRotation, correctFormat.localRotation) <= 10)
            {
                target.localRotation = targetLocalRotation;
                Debug.Log("Good Job");
                return true;
            }
            target.Rotate(Vector3.forward,Space.World);
        }
        target.localRotation = targetLocalRotation;
        return true;
    }

    void  checkRotaionisCorrect(Transform target)
    {
        Quaternion targetLocalRotation = target.rotation;
        Quaternion correctRot = Quaternion.Euler(0,0,0);
        Quaternion checkRotation = Quaternion.Euler(targetLocalRotation.eulerAngles.x,
                                    targetLocalRotation.eulerAngles.y,
                                    correctRot.eulerAngles.z);
        bool isRotationCloseEnough = Quaternion.Angle(checkRotation, correctRot) <= 2f;
        Debug.Log($"Angle {checkRotation.eulerAngles} {correctRot.eulerAngles} :"+Quaternion.Angle(checkRotation, correctRot));
        if(isRotationCloseEnough) Debug.Log("so yeah Good Job");
    }
    
}

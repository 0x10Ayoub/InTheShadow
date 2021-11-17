using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class ShadowObject : MonoBehaviour
{

    public bool isInCorrectForm;
    [SerializeField] private Transform shadowOnly;
    [SerializeField] private Transform meshOnly;
    [SerializeField] private Transform correctFormat;
    [SerializeField] private Transform lightPoint;
    [SerializeField] private float rotationTolerance;
    [SerializeField] private float positionTolerance;
    [SerializeField] private bool canMoveHorizontally;
    [SerializeField] private bool canMoveVertically;
    [SerializeField] private bool canRotateHorizontally;
    [SerializeField] private bool canRotateVertically;
    [Header("-----End Level Variable------")]
    [SerializeField] private float lerpRotationSpeed = 1f;
    [SerializeField] private float lerpPositionSpeed = 10f;
    
    private Vector3 _initNormal;
    private Vector3 _initPosition;
    private Vector3 _lightPointOffset;
    private float _lightPointDistance;
    private MeshRenderer _meshRenderer;
    private Material _defaultMat;

    private void Awake()
    {
        _meshRenderer = meshOnly.GetComponentInChildren<MeshRenderer>();
        _defaultMat = _meshRenderer.material;
    }

    private void Start()
    {
        isInCorrectForm = false;
        _initPosition = correctFormat.position;
        _lightPointDistance = Vector3.Distance(correctFormat.position,lightPoint.position);
        _initNormal = correctFormat.forward; //-(lightPoint.position - correctFormat.position).normalized;
        _lightPointOffset = lightPoint.position - correctFormat.position;
        _lightPointOffset.z = 0;
    }

    public void Move(Vector3 moveDirection,float movingSpeed)
    {
        float moveDeltaSpeed = movingSpeed * Time.deltaTime;
        Vector3 Horizontal = canMoveHorizontally ? Vector3.right : Vector3.zero;
        Vector3 Vertical = canMoveVertically ? Vector3.up : Vector3.zero;
        Vector3 movingDelta = Vertical * (moveDirection.y * moveDeltaSpeed) 
                              + Horizontal * (moveDirection.x * moveDeltaSpeed);
        shadowOnly.Translate(movingDelta,Space.World);
        meshOnly.Translate(movingDelta,Space.World);
        correctFormat.Translate(movingDelta,Space.World);
        correctFormat.forward = -(lightPoint.position - correctFormat.position - _lightPointOffset).normalized;
    }

    public void Rotate(Vector3 rotationDir,float rotationSpeed)
    {
        Vector3 rotHorizontal = canRotateHorizontally ? Vector3.up * (-rotationDir.x * rotationSpeed) : Vector3.zero;
        Vector3 rotVertical = canRotateVertically ? Vector3.right * (rotationDir.y * rotationSpeed) : Vector3.zero;
        Vector3 rotationDelta = (rotHorizontal + rotVertical) * Time.deltaTime;
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

    public bool CheckIfPositionCorrect()
    {
        float distance = Vector3.Distance(shadowOnly.position, _initPosition);
        Debug.Log("distance" + distance);
        return distance < positionTolerance;
    }
    public IEnumerator LerpToCorrectRotation()
    {
        correctFormat.forward = _initNormal;
        float angle = Quaternion.Angle(correctFormat.rotation, shadowOnly.rotation);
        angle = angle > 2 ? angle : rotationTolerance;
        float deltaTimeSpeed = Time.deltaTime / (angle * lerpRotationSpeed);
        float time = 0f;
        while (time <= 1f)
        {
            Quaternion q = Quaternion.Lerp(shadowOnly.rotation, correctFormat.rotation, time);
            meshOnly.rotation = q;
            shadowOnly.rotation = q;
            time += deltaTimeSpeed;
            yield return null;
        }
    }

    public IEnumerator LerpToCorrectPosition()
    {
        float distance = Vector3.Distance(_initPosition, shadowOnly.position);
        distance = distance > 0 ? distance * lerpPositionSpeed : lerpPositionSpeed;
        float deltaTimeSpeed = Time.deltaTime / distance;
        float time = 0f;
        while (time <= 1f)
        {
            Vector3 newPos = Vector3.Lerp(shadowOnly.position, _initPosition, time);
            correctFormat.position = newPos;
            shadowOnly.position = newPos;
            newPos.z = meshOnly.position.z;
            meshOnly.position = newPos;
            time += deltaTimeSpeed;
            yield return null;
        }
    }

    public IEnumerator LerpEmission(Material s)
    {
        Color meshcolor = Color.white;
        Color originalColor = _meshRenderer.material.color;
        Debug.Log($" Hi {meshcolor.r} {meshcolor.g} {meshcolor.b}");
        float time = 0f;
        while (time < 2f)
        {
            float speed = Time.deltaTime ;
            time += speed;
            originalColor = Color.Lerp(originalColor, meshcolor, time);
            _meshRenderer.material.SetColor("_Color", originalColor);
            yield return null;
        }
        
        yield return null;
    }
    public void Select(Material selectMat)
    {
        _meshRenderer.material = selectMat;
    }
    
    
    public void DeSelect()
    {
        _meshRenderer.material = _defaultMat;
    }

    
}

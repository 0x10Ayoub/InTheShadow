using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform light;
    [Range(10, 50)] public int moveSpeed;
    [Range(1,5)]
    public int timeFactor;
    private Vector3 offset;
    private Vector3 newPosition;
    private Transform _camTransform;
    private void Start()
    {
        offset = Vector3.right * 5;
        _camTransform = cam.transform;
    }
    private void MoveCameraAndLight()
    {
        if (!Input.GetMouseButton(1)) return;
        float x = Input.GetAxis("Mouse X");
        Vector3 moveOffset = Vector3.right * -x * Time.deltaTime * moveSpeed;
        Vector3 camNewPos = _camTransform.position + moveOffset;
        if(camNewPos.x <-1 || camNewPos.x >20) return;
        cam.transform.position = camNewPos;
        light.transform.position += moveOffset;
    }

    IEnumerator LerpToPosition()
    {
        float time = 0f;
        newPosition = cam.transform.position + Vector3.right * 5;
        Vector3 lightNewPos = light.position + Vector3.right * 7 ;
        while (time <= 1)
        {
            time += Time.deltaTime / timeFactor;
            _camTransform.position = Vector3.Lerp(_camTransform.position, newPosition, Time.deltaTime / timeFactor);
            light.position = Vector3.Lerp( light.position, lightNewPos, Time.deltaTime / timeFactor);
            yield return null;
        }
    }

    public void StartEffect()
    {
        StartCoroutine(LerpToPosition());
    }

    private void Update()
    {
        MoveCameraAndLight();
    }
}

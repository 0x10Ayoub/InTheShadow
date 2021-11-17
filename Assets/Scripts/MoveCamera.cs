using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform light;
    
    [Range(1,5)]
    public int timeFactor;
    private Vector3 offset;
    private Vector3 newPosition;
    private Transform _transform;
    private void Start()
    {
        offset = Vector3.right * 5;
        _transform = cam.transform;
    }

    IEnumerator LerpToPosition()
    {
        float time = 0f;
        newPosition = cam.transform.position + Vector3.right * 5;
        Vector3 lightNewPos = light.position + Vector3.right * 7 ;
        while (time <= 1)
        {
            time += Time.deltaTime / timeFactor;
            _transform.position = Vector3.Lerp(_transform.position, newPosition, Time.deltaTime / timeFactor);
            light.position = Vector3.Lerp( light.position, lightNewPos, Time.deltaTime / timeFactor);
            yield return null;
        }
    }

    public void StartEffect()
    {
        StartCoroutine(LerpToPosition());
    }
}

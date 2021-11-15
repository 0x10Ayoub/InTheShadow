using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayControl : MonoBehaviour
{

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movingSpeed;
    [SerializeField] private ShadowObject[] shadowObjects;

    private bool _gameEnded;
    private int _objectIndex;
    private Vector3 _dir;

    private void Start()
    {
        _gameEnded = false;
        _dir = Vector3.zero;
        rotationSpeed *= Time.deltaTime;
        movingSpeed *= Time.deltaTime;
        _objectIndex = 0;
    }

    private void Update()
    {
        SetInput();
        Move(shadowObjects[_objectIndex]);
    }

    private void Move(ShadowObject target)
    {
        if(!Input.GetMouseButton(0) || _gameEnded) return;
        target.Rotate(_dir,rotationSpeed);
        if (target.CheckIfRotationCorrect())
        {
            _gameEnded = true;
            StartCoroutine(target.LerpToRotation());
        }
    }

    private void SetInput()
    {
        _dir.x = Input.GetAxis("Mouse X");
        _dir.y = Input.GetAxis("Mouse Y");
    }
    
}

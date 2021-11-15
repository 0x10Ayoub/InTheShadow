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
        _objectIndex = 0;
    }

    private void Update()
    {
        SetInput();
        Control();
    }

    private void Rotate(ShadowObject target)
    {
        if(!Input.GetMouseButton(0) || _gameEnded) return;
        target.Rotate(_dir * 10f,rotationSpeed);
    }

    private void Control()
    {
        if(!Input.GetMouseButton(0) || _gameEnded) return;
        ShadowObject target = shadowObjects[_objectIndex];
        if(Input.GetKey(KeyCode.LeftControl))
            Move(target);
        else
            Rotate(target);
        if (target.CheckIfRotationCorrect() && target.CheckIfPositionCorrect())
        {
            _gameEnded = true;
            //StartCoroutine(target.LerpToCorrectRotation());
            StartCoroutine(target.LerpToCorrectPosition());
        }
        
    }
    private void Move(ShadowObject target)
    {
        if(!Input.GetMouseButton(0) || _gameEnded) return;
        target.Move(_dir,movingSpeed);
    }

    private void SetInput()
    {
        _dir.x = Input.GetAxis("Mouse X");
        _dir.y = Input.GetAxis("Mouse Y");
    }
    
}

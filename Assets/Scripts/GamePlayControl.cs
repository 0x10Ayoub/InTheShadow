using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayControl : MonoBehaviour
{

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movingSpeed;
    [SerializeField] private ShadowObject[] shadowObjects;
    [SerializeField] private Material selectedMat;
    private bool _gameEnded;
    private int _objectIndex;
    private Vector3 _dir;
    private void Start()
    {
        _gameEnded = false;
        _dir = Vector3.zero;
        _objectIndex = 0;
        ShadowObject target = shadowObjects[_objectIndex];
        target.Select(selectedMat);
    }

    private void Update()
    {
        SetInput();
        Control();
        
    }

    private void Rotate(ShadowObject target)
    {
        if(!Input.GetMouseButton(0) || _gameEnded) return;
        target.Rotate(_dir,rotationSpeed);
    }

    private void Control()
    {
        if(!Input.GetMouseButton(0) || _objectIndex == -1) return;
        ShadowObject target = shadowObjects[_objectIndex];
        if(Input.GetKey(KeyCode.LeftControl))
            Move(target);
        else
            Rotate(target);
        if (target.CheckIfRotationCorrect() && target.CheckIfPositionCorrect())
        {
            target.isInCorrectForm = true;
            target.DeSelect();
            StartCoroutine(target.LerpToCorrectRotation());
            StartCoroutine(target.LerpToCorrectPosition());
            _objectIndex = IsAnyObjectNotInCorrectForm();
            if(_objectIndex == -1)
                _gameEnded = true;
            else
                shadowObjects[_objectIndex].Select(selectedMat);
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

    private int IsAnyObjectNotInCorrectForm()
    {
        for (int i = 0; i < shadowObjects.Length; i++)
        {
            if (!shadowObjects[i].isInCorrectForm)
                return i;
        }
        return -1;
    }
    
}

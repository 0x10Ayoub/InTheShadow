using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePlayControl : MonoBehaviour
{

    [SerializeField] private Text msgText;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movingSpeed;
    [SerializeField] private ShadowObject[] shadowObjects;
    [SerializeField] private Material selectedMat;
    [SerializeField] private ParticleSystem particlesEffect;
    [SerializeField] private AudioSource sfx;
    [SerializeField] private AudioSource ambient;
    [SerializeField] private UIManager uiManger;
    [SerializeField] private LevelInfo info;
    [SerializeField] private LevelTransitionData _data;

    [SerializeField] private UnityEvent Endleve;

    private bool _gameEnded;
    private int _objectIndex;
    private Vector3 _dir;
    private bool isMsgVisible;
    private void Start()
    {
        _gameEnded = false;
        _dir = Vector3.zero;
        _objectIndex = 0;
        isMsgVisible = false;
        ShadowObject target = shadowObjects[_objectIndex];
        target.Select(selectedMat);
    }

    private void Update()
    {
        SelectObject();
        SetInput();
        Control();
        
    }

    private void SelectObject()
    {
        if (!Input.GetMouseButtonDown(1) || _gameEnded || _objectIndex == -1) return;
        ShadowObject target = shadowObjects[_objectIndex];
        target.DeSelect();
        _objectIndex = (_objectIndex + 1) % shadowObjects.Length;
        target = shadowObjects[_objectIndex];
        target.Select(selectedMat);
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
            if (_objectIndex == -1)
            {
                _gameEnded = true;
                StartCoroutine(SetEndLevel());
            }
            else
            {
                shadowObjects[_objectIndex].Select(selectedMat);
            }
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

    public void SetHint(string hint)
    {
        isMsgVisible = !isMsgVisible;
        msgText.text = isMsgVisible ? hint : "";
    }

    public void SetLevelInfo()
    {
        if (!info.isSolved && !_data.isTestMode)
        {
            info.isSolved = true;
            info.isUnlocked = true;
            info.isRecentlySolved = true;
            info.Save();
        }
        Debug.Log(JsonUtility.ToJson(info,true));
    }
    private IEnumerator SetEndLevel()
    {
        SetLevelInfo();
        Endleve.Invoke();
        particlesEffect.Play();
        sfx.PlayOneShot(sfx.clip);
        yield return new WaitForSeconds(2);
        uiManger.Pause();
    }
    
}

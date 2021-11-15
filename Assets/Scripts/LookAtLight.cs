using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtLight : MonoBehaviour
{
    public Transform lightTran;
    public Transform meshData;
    private Vector3 _dir;

    private float _movingSpeed;

    private Vector3 _initForward;

    private Vector3 InitPosition;

    private float _lightOffset;

    private Vector3 _lightPos;
    // Start is called before the first frame update
    void Start()
    {
        InitPosition = transform.position;
        _initForward = meshData.forward;
        _dir = lightTran.position - meshData.position;
        _movingSpeed = 50f * Time.deltaTime;
        _lightOffset = Vector3.Distance(meshData.position,lightTran.position);
        _lightPos = meshData.forward * _lightOffset + meshData.position;
        Debug.Log(_lightOffset);
        Instantiate(meshData, meshData.position+_lightPos, meshData.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Input.GetMouseButton(0)) return;
        var x =  Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");
        if (Input.GetKey(KeyCode.LeftControl) )
        {
           Vector3 movingDelta = Vector3.up * y * _movingSpeed + Vector3.right * x * _movingSpeed;
           meshData.Translate(movingDelta,Space.World);
           Vector3 newNormal =(_lightPos - meshData.position).normalized;
            meshData.forward = newNormal;
        }
    }
}

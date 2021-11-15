using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{

    public Rotation[] objects;

    private int _index;
    // Start is called before the first frame update
    void Start()
    {
        _index = 0;
        SetObjectRotationActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            _index++;
            _index %= objects.Length;
            SetObjectRotationActive();
        }
    }

    private void SetObjectRotationActive()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (i == _index)
                objects[i].enabled = true;
            else
                objects[i].enabled = false;
        }
    }
}

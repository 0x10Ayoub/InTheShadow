using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItem : MonoBehaviour
{
    public LevelInfo info;
    public Texture icon;
    public Material mat;
    public MeshRenderer quadMesh;
    public MeshRenderer mainObjectMesh;
    public void SetInfo()
    {
        mainObjectMesh.material = mat;
        quadMesh.material.mainTexture = icon;
    }
}

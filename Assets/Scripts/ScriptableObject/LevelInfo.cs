using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelInfo",menuName = "So/LevelInfo")]
public class LevelInfo : ScriptableObject
{
    public Texture icon;
    public bool isSolved;
    public bool isUnlocked;
    public bool isRecentlySolved;
    public EScenesIndex sceneIndex;

    private ObjectData _data;
    private void OnEnable()
    {
        _data = new ObjectData();
        hideFlags = HideFlags.DontUnloadUnusedAsset;
        Read();
        //Save();
    }

    public void Save()
    {
        string path = Application.persistentDataPath + $"/{sceneIndex.ToString()}";
        _data.SetObjectData(isSolved,isUnlocked,isRecentlySolved,sceneIndex);
        string serialzedData = JsonUtility.ToJson(_data, true);
        File.WriteAllText(path,serialzedData);
    }

    public void Read()
    {
        string path = Application.persistentDataPath + $"/{sceneIndex.ToString()}";
        if(!File.Exists(path)) return;
        string deserializedData = File.ReadAllText(path);
        if(String.IsNullOrEmpty(deserializedData)) return;
        _data = JsonUtility.FromJson<ObjectData>(deserializedData);
        SetData();
        //Debug.Log(deserializedData);
    }

    public void ResetData()
    {
        this.isSolved = false;
        this.isUnlocked = sceneIndex == EScenesIndex.TeaPos;
        this.isRecentlySolved = false;
        _data.SetObjectData(isSolved,isUnlocked,isRecentlySolved,sceneIndex);
        Save();
        SetData();
    }
    private void SetData()
    {
        this.isSolved = _data.isSolved;
        this.isUnlocked = _data.isUnlocked;
        this.isRecentlySolved = _data.isRecentlySolved;
    }

    public void DeleteData()
    {
        string path = Application.persistentDataPath + $"/{sceneIndex.ToString()}";
        if(!File.Exists(path)) return;
        File.Delete(path);
    }
}

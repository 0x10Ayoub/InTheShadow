using System;
using System.Collections;
using System.Collections.Generic;
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

    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
    }
}

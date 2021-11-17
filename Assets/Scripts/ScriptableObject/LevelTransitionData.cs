using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelTransitionData",menuName = "So/LevelTransitionData")]
public class LevelTransitionData : ScriptableObject
{
    public int SceneToLoad;

    private void OnDestroy()
    {
        SceneToLoad = 0;
    }
}

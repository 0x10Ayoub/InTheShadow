using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconLevelManager : MonoBehaviour
{
    [SerializeField] private LevelItem prefab;
    [SerializeField] private Material solvedMat;
    [SerializeField] private Material lockedMat;
    [SerializeField] private Material selectedMat;
    [SerializeField] private Texture unlockedIcon;
    [SerializeField] private LevelInfo[] levelInfos;

    private void Start()
    {
        Vector3 offest = Vector3.zero; 
        for (int i = 0; i < levelInfos.Length; i++)
        {
            var levelIcon = Instantiate(prefab, transform.position , prefab.transform.rotation);
            levelIcon.transform.SetParent(transform);
            levelIcon.transform.localPosition = offest;
            SetIcon(levelInfos[i], levelIcon);
            offest += Vector3.right * 5f;
        }
    }
    
    private void SetIcon(LevelInfo levelInfo,LevelItem levelItem)
    {
        levelItem.info = levelInfo;
        if (levelInfo.isSolved)
        {
            levelItem.icon = levelInfo.icon;
            levelItem.mat = solvedMat;
            levelItem.SetInfo();
        }else if (levelInfo.isUnlocked)
        {
            levelItem.icon = unlockedIcon;
            levelItem.mat = lockedMat;
            levelItem.SetInfo();
        }
       
    }
}

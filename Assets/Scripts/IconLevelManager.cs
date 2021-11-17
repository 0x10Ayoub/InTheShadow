using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IconLevelManager : MonoBehaviour
{
    [SerializeField] private LevelItem prefab;
    [SerializeField] private Material solvedMat;
    [SerializeField] private Material lockedMat;
    [SerializeField] private Material selectedMat;
    [SerializeField] private Texture unlockedIcon;
    [SerializeField] private LevelInfo[] levelInfos;
    [SerializeField] private UnityEvent recentlySolvedEvent;
    [SerializeField] private Transform came;
    [SerializeField] private Transform light;


    private bool cameraSet;
    private void Start()
    {
        cameraSet = true;
        Vector3 offest = Vector3.zero;
        bool isRecentlySolved = false;
        Vector3 tmpPos;
        for (int i = 0; i < levelInfos.Length; i++)
        {
            var levelIcon = Instantiate(prefab, transform.position , prefab.transform.rotation);
            levelIcon.transform.SetParent(transform);
            levelIcon.transform.localPosition = offest;
            LevelInfo levelInfo = levelInfos[i];
            bool caneSetCamPos = cameraSet && ((levelInfo.isUnlocked && !levelInfo.isSolved)
                                               || levelInfo.isRecentlySolved);
            if (caneSetCamPos)
            {
                tmpPos = levelIcon.transform.position;
                tmpPos.z = came.position.z;
                came.position = tmpPos;
                tmpPos.z = light.position.z;
                light.position = tmpPos;
                cameraSet = false;
            }
            if (levelInfo.isRecentlySolved)
            {
                levelInfo.isRecentlySolved = false;
                isRecentlySolved = true;
            }else if (isRecentlySolved)
            {
                levelInfo.icon = unlockedIcon;
                levelInfo.isUnlocked = true;
                isRecentlySolved = false;
                recentlySolvedEvent.Invoke();
            }
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

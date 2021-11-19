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
    [SerializeField] private LevelTransitionData leveldata;

    private LevelManager _levelManager;
    private bool cameraSet;
    private void Start()
    {
        _levelManager = GetComponent<LevelManager>();
        SpawnIcons();
    }


    private void SpawnIcons()
    {
        cameraSet = true;
        Vector3 offest = Vector3.zero;
        bool isRecentlySolved = false;
        for (int i = 0; i < levelInfos.Length; i++)
        {
            var levelIcon = Instantiate(prefab, transform.position , prefab.transform.rotation);
            levelIcon.transform.SetParent(transform);
            levelIcon.transform.localPosition = offest;
            LevelInfo levelInfo = levelInfos[i];
            bool caneSetCamPos = cameraSet && ((levelInfo.isUnlocked && !levelInfo.isSolved) 
                                               || levelInfo.isRecentlySolved);
            SetCameraAndLightPosition(caneSetCamPos,levelIcon);
            if (levelInfo.isRecentlySolved && !leveldata.isTestMode)
            {
                levelInfo.isRecentlySolved = false;
                isRecentlySolved = true;
                levelInfo.Save();
            }else if (isRecentlySolved && !leveldata.isTestMode)
            {
                levelIcon.icon = unlockedIcon;
                levelInfo.isUnlocked = true;
                isRecentlySolved = false;
                levelInfo.Save();
                recentlySolvedEvent.Invoke();
            }
            SetIcon(levelInfo, levelIcon);
            offest += Vector3.right * 5f;
        }
    }

    private void SetCameraAndLightPosition(bool caneSetCamPos,LevelItem levelIcon)
    {
        Vector3 tmpPos;
        if(leveldata.isTestMode) return;
        if (caneSetCamPos)
        {
            tmpPos = levelIcon.transform.position;
            tmpPos.z = came.position.z;
            came.position = tmpPos;
            tmpPos.z = light.position.z;
            light.position = tmpPos;
            cameraSet = false;
        }  
    }
    private void SetIcon(LevelInfo levelInfo,LevelItem levelItem)
    {
        levelItem.info = levelInfo;
        if (levelInfo.isSolved || leveldata.isTestMode)
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

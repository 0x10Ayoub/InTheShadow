﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EScenesIndex
{
   MainMenu = 0,
   TeaPos = 1,
   Elephant = 2,
   FortyTwo = 3,
   GLobe = 4,
   ThirteenSeven = 5,
   selectLevelScenes=6,
   TransitionScenes=7
}
public class LevelManager : MonoBehaviour
{

   public Camera main;
   [SerializeField] private LevelTransitionData leveldata;
   public void LoadLevel()
   {
      
   }

   private void Update()
   {
      SelectLevel();
   }

   private void SelectLevel()
   {
      if (!Input.GetMouseButtonDown(0)) return;
      RaycastHit hit;
      Ray ray = main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit))
      {
         LevelItem levelInfo = hit.transform.GetComponent<LevelItem>();
         if (levelInfo != null)
         {
            leveldata.SceneToLoad = (int)levelInfo.info.sceneIndex;
            StartCoroutine(LoadLevelAsyn((int)EScenesIndex.TransitionScenes));
         }
      }
   }
   public void LoadTargetLevel(int ScenesIndex)
   {
      leveldata.SceneToLoad = ScenesIndex;
      StartCoroutine(LoadLevelAsyn((int)EScenesIndex.TransitionScenes));
   }
   IEnumerator LoadLevelAsyn(int levelIndex)
   {
      AsyncOperation op = SceneManager.LoadSceneAsync(levelIndex);
      while (op.isDone)
      {
         yield return null;
      }
   }
}

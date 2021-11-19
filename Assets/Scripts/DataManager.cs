using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public Text msg;
    public LevelInfo[] levelsConfig;
    
    public void ResetAllData()
    {
        for (int i = 0; i < levelsConfig.Length; i++)
        {
            levelsConfig[i].ResetData();
            //levelsConfig[i].DeleteData();
        }
        msg.text = "Data Deleted successfully";
    }

}

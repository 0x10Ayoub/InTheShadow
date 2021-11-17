using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UIElementEnum
{
    GamePlay = 0,
    Paused = 1
}

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] UIElement;

    public void Pause()
    {
        SetCorrectUi(UIElementEnum.Paused);
    }
    
    public void Play()
    {
        SetCorrectUi(UIElementEnum.GamePlay);
    }

    private void SetCorrectUi(UIElementEnum UIEnum)
    {
        int indexFfUi = (int) UIEnum;
        for (int i = 0; i < UIElement.Length; i++)
        {
            UIElement[i].SetActive(indexFfUi == i);
        }
    }
}

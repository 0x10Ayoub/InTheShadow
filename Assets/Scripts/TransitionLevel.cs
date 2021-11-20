using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionLevel : MonoBehaviour
{
    public LevelTransitionData levelTransitionData;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(levelTransitionData.SceneToLoad, LoadSceneMode.Single);
    }
}

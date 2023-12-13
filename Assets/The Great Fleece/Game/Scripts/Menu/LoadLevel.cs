using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public Image loadingBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");

        while(operation.isDone == false)
        {
            loadingBar.fillAmount = operation.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider s;
    public void LoadLevel(int sceneIdx)
    {
        StartCoroutine(LoadAsynchronously(sceneIdx));
    }

    IEnumerator LoadAsynchronously(int sceneIdx)
    {
        AsyncOperation oprt = SceneManager.LoadSceneAsync(sceneIdx);

        loadingScreen.SetActive(true);
        while (!oprt.isDone)
        {
            float progress = Mathf.Clamp01(oprt.progress / .9f);
            Debug.Log(progress);

            s.value = progress;

            yield return null;
        }

    }
}

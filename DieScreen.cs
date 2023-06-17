using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieScreen : MonoBehaviour
{
    public GameObject dScreen;
    public GameObject loadingScreen, bg;
    public Slider s;
    IEnumerator LoadAsynchronously(int sceneIdx)
    {
        AsyncOperation oprt = SceneManager.LoadSceneAsync(sceneIdx);

        loadingScreen.SetActive(true);
        bg.SetActive(true);
        while (!oprt.isDone)
        {
            float progress = Mathf.Clamp01(oprt.progress / .9f);
            Debug.Log(progress);

            s.value = progress;

            yield return null;
        }

    }
    public void Exit()
    {
        dScreen.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(LoadAsynchronously(2));
    }
}

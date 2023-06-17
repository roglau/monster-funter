using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerSpotlight : MonoBehaviour
{
    public Light spotlight;
    public GameObject c, k, p;
    private PlayerObject instance = PlayerObject.GetInstance();
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

    //private void Awake()
    //{
    //    DontDestroyOnLoad(spotlight);
    //}
    public void OnMouseEnter()
    {
        spotlight.enabled = true;
    }

    public void OnMouseExit()
    {
        spotlight.enabled = false;
    }

    public void OnMouseDown()
    {
        if(c == k)
        {
            instance.SetIdx(0);
        }
        else
        {
            instance.SetIdx(1);
        }
        Debug.Log(instance.GetIdx());
        StartCoroutine(LoadAsynchronously(2));
    }


}

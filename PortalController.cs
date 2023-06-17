using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalController : MonoBehaviour
{
    public GameObject textContainer;
    public TextMeshProUGUI text;
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
    private void OnTriggerStay(Collider other)
    {
        if(instance.CurrMission >= 3)
        {
            textContainer.SetActive(true);
            text.text = "Press [J] to go to portal";
            if (Input.GetKeyDown(KeyCode.J))
            {
                StartCoroutine(LoadAsynchronously(3));
            }
        }
        else
        {
            textContainer.SetActive(true);
            text.text = "Complete missions first!";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        textContainer.SetActive(false);
    }
}

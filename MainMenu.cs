using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Back()
    {
        SceneManager.LoadScene(0);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        //Debug.Log("keluar");
        PlayerPrefs.SetFloat("musicV", 1);
        PlayerPrefs.SetFloat("audioV", 1);
        PlayerPrefs.Save();
    }
}

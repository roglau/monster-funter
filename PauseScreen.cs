using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    public GameObject pScreen;
    private bool isPaused = false;
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (isPaused) {
                Resume();
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                pScreen.SetActive(true);
                Time.timeScale = 0f;
                isPaused = true;
            }
            

        }
        
    }

    public void Resume()
    {
        pScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Exit()
    {
        Debug.Log("keluar cok");
        pScreen.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroyAudio : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] music = GameObject.FindGameObjectsWithTag("GameMusic");

        DontDestroyOnLoad(this.gameObject);
    }
}

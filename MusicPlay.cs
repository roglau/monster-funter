using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class MusicPlay : MonoBehaviour
{
    public GameObject inside, outside;
    int c = 1;
    private void OnTriggerEnter(Collider other)
    {
        if (c %2 == 1)
        {
            inside.SetActive(false);
            outside.SetActive(true);
            c++;
        }
        else
        {
            inside.SetActive(true);
            outside.SetActive(false);
            c++;
        }
        
    }
}

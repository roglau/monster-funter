using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMenuSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DoNotDestroyAudio dd = Object.FindObjectOfType<DoNotDestroyAudio>();
        if (dd != null)
        {
            Destroy(dd.gameObject);
        }
    }

}

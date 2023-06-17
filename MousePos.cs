using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    public Camera main;
    void Update()
    {
        Ray ray = main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit)){
            transform.position = hit.point;
        }
    }
}

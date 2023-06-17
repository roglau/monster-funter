using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PlayerObject instance = PlayerObject.GetInstance();
    public GameObject p, k;
    private GameObject selected;
    public CinemachineFreeLook virtualCamera;
    //[SerializeField]
    //private Animator _animator;

    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2 && instance.Died)
        {
            instance.ResetAfterDie();
            instance.Died = false;
        }else if (SceneManager.GetActiveScene().buildIndex == 2 && !instance.Died)
        {
            instance.Reset();
        }
        k.SetActive(false);
        p.SetActive(false);
        if (instance.GetIdx() == 0)
        {
            selected = k;
            //k.SetActive(true);
            //p.SetActive(false);
        }
        else
        {
            selected = p;
            //k.SetActive(false);
            //p.SetActive(true);
        }
        //Vector3 rsp = selected.
        //GameObject g = Instantiate( selected , transform.position, Quaternion.identity);
        
        instance.P = selected;
        selected.SetActive(true);
        //Debug.Log(g.transform);
        virtualCamera.Follow = selected.transform;
        virtualCamera.LookAt = selected.transform;
        //g.transform.LookAt(g.transform.position);
    }

}

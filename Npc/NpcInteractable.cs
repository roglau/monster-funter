using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NpcInteractable : MonoBehaviour
{
    public GameObject interactiveText, dialog;
    public TextMeshProUGUI dialogText, missionText;
    private PlayerObject instance = PlayerObject.GetInstance();
    private string[] dText = { "Use Basic Attack 10 Time", "Use all skill available","Talk to all people in the village", "Go to portal" };
    private string[] mText = { "Instantiate Attack ", "Use your skill ability ", "Talk to all people", "Go to portal" };
    public static int[] progress = { 0, 0, 1 };
    public static bool skill1, skill2;
    public static bool dar, ces;

    public void Interact()
    {
        //interactiveText.SetActive(true);
        //Debug.Log("interact!");
    }

    private void Start()
    {
        skill1 = false;
        skill2 = false;
        dar = false;
        ces = false;
        dialogText.text = dText[instance.CurrMission];
        instance.OnProgress = false;
    }

    private void Update()
    {
        Debug.Log("Progress" + progress[0]);
        Debug.Log("Mission" + instance.CurrMission);
        if (instance.CurrMission == 0)
        {
            if(progress[0] == 10)
            {
                missionText.color = Color.green;
                missionText.text = mText[instance.CurrMission] + " " + progress[instance.CurrMission] + " / 10";
            }
            if (instance.OnProgress)
            {
                missionText.text = mText[instance.CurrMission] + " " + progress[instance.CurrMission] + " / 10";
            }
        }
        if (instance.CurrMission == 1)
        {
            if (progress[1] == 2)
            {
                missionText.color = Color.green;
                missionText.text = mText[instance.CurrMission] + " " + progress[instance.CurrMission] + " / 2";
            }
            if (instance.OnProgress)
            {
                missionText.text = mText[instance.CurrMission] + " " + progress[instance.CurrMission] + " / 2";
            }
        }
        if(instance.CurrMission == 2)
        {
            if(progress[2] == 3)
            {
                missionText.color = Color.green;
                missionText.text = mText[instance.CurrMission] + " " + progress[instance.CurrMission] + " / 3";
            }
            if (instance.OnProgress)
            {
                missionText.text = mText[instance.CurrMission] + " " + progress[instance.CurrMission] + " / 3";
            }
        }

        if(instance.CurrMission == 3)
        {
            missionText.text = mText[instance.CurrMission];
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactiveText.SetActive(true);
            NpcController.StateInteract();
            if (Input.GetKeyDown(KeyCode.C) && gameObject.tag == "Lyra")
            {
                if(instance.CurrMission == 0)
                {
                    if(missionText.color == Color.green)
                    {
                        instance.CurrMission++;
                        instance.OnProgress = false;
                        missionText.color = Color.white;
                    }
                    dialogText.text = dText[instance.CurrMission];
                    instance.OnProgress = true;
                    dialog.SetActive(true);
                    Invoke("DialogFalse", 3f);
                    missionText.text = mText[instance.CurrMission] + " "+progress[instance.CurrMission] + " / 10";

                }
                
                if(instance.CurrMission == 1)
                {
                    if (missionText.color == Color.green)
                    {
                        instance.CurrMission++;
                        instance.OnProgress = false;
                        missionText.color = Color.white;
                    }
                    dialogText.text = dText[instance.CurrMission];
                    instance.OnProgress = true;
                    dialog.SetActive(true);
                    Invoke("DialogFalse", 3f);
                    missionText.text = mText[instance.CurrMission] + " " + progress[instance.CurrMission] + " / 2";
                }

                if (instance.CurrMission == 2)
                {
                    
                    dialogText.text = dText[instance.CurrMission];
                    instance.OnProgress = true;
                    dialog.SetActive(true);
                    Invoke("DialogFalse", 3f);
                    missionText.text = mText[instance.CurrMission] + " " + progress[instance.CurrMission] + " / 3";

                    if (missionText.color == Color.green)
                    {
                        instance.CurrMission++;
                        instance.OnProgress = false;
                        missionText.color = Color.white;
                    }
                }

                if(instance.CurrMission == 3)
                {
                    dialogText.text = dText[instance.CurrMission];
                    instance.OnProgress = true;
                    dialog.SetActive(true);
                    Invoke("DialogFalse", 3f);
                    missionText.text = mText[instance.CurrMission];
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.C) && gameObject.tag == "Darian")
        {
            if(instance.CurrMission == 2 && !dar && instance.OnProgress)
            {
                dar = true;
                progress[instance.CurrMission]++;
            }
        }

        if (Input.GetKeyDown(KeyCode.C) && gameObject.tag == "Cesiya")
        {
            if (instance.CurrMission == 2 && !ces && instance.OnProgress)
            {
                ces = true;
                progress[instance.CurrMission]++;
            }
        }
    }

    public void DialogFalse()
    {
        dialog.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        interactiveText.SetActive(false);
        NpcController.StateIdle();
    }
}

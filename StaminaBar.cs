using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider s;
    private PlayerObject instance = PlayerObject.GetInstance();
    void Start()
    {
        s.value = instance.GetCurrStamina();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            s.value -= 0.5f;
            //Debug.Log(s.value);
        }
        else
        {
            s.value += 1f;
            //Debug.Log(s.value);
        }
    }
}

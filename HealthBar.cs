using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    private PlayerObject instance = PlayerObject.GetInstance();
    public Slider s;

    void Start()
    {
        s.value = instance.GetCurrHp();
    }

    // Update is called once per frame
    void Update()
    {
        s.value = instance.GetCurrHp();
        
    }
}

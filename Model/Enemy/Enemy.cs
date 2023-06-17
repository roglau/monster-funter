using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private float currHp;
    public Slider health;
    private EnemyController ec;
    private void Start()
    {
        currHp = 30f;
        health.value = currHp;
        ec = gameObject.GetComponent<EnemyController>();
    }

    private void Update()
    {
        if(health.value == 0)
        {
            EnemyController.StateDie();
        }
        health.value = currHp;

    }

    public float CurrHp
    {
        get { return currHp; }
        set { currHp = value; }
    }
}

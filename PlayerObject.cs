using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject
{
    private static PlayerObject instance = null;

    private int idx;
    private float maxHealth = 100f, maxStamina = 100f;
    private float currHealth, currStamina, damage;
    private bool damaged;
    private int meat, healthPotion;
    private GameObject p;
    private float flyCd = 10f, fireCd = 5f, rollCd = 3f, rageCd = 5f;
    private int currMission;
    private bool onProgress;
    private bool died;

    private PlayerObject()
    {
        died = false;
        currHealth = maxHealth;
        currStamina = maxStamina;
        meat = 1;
        healthPotion = 2;
        currMission = 0;
        onProgress = false;
    }

    public void ResetAfterDie()
    {
        currHealth = maxHealth;
        currStamina = maxStamina;
    }

    public bool Died
    {
        get { return died; }
        set { died = value; }
    }

    public void Reset()
    {
        currHealth = maxHealth;
        currStamina = maxStamina;
        meat = 1;
        healthPotion = 2;
        currMission = 0;
        onProgress = false;
    }

    public bool OnProgress
    {
        get { return onProgress; }
        set { onProgress = value; }
    }

    public int CurrMission{
        get { return currMission; }
        set { currMission = value; }
    }

    public GameObject P
    {
        get { return p; }
        set { p = value; }
    }
    public float FlyCd
    {
        get { return flyCd;}
        set { flyCd = value; }
    }

    public float FireCd
    {
        get { return fireCd; }
        set { fireCd = value; }
    }

    public float RollCd
    {
        get { return rollCd; }
        set { rollCd = value; }
    }

    public float RageCd
    {
        get { return rageCd; }
        set { rageCd = value; }
    }

    public int GetMeat()
    {
        return meat;
    }

    public void SetMeat(int m)
    {
        meat = m;
    }

    public int GetHealthPotion()
    {
        return healthPotion;
    }

    public void SetHealthPotion(int h)
    {
        healthPotion = h;
    }
   

    public static PlayerObject GetInstance()
    {
        if(instance == null)
        {
            instance = new PlayerObject();
        }

        return instance;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float d)
    {
        damage = d;
    }

    public bool GetDamaged()
    {
        return damaged;
    }
    public void SetDamaged(bool b)
    {
        damaged = b;
    }

    public float GetCurrHp()
    {
        return currHealth;
    }

    public void SetCurrHp(float hp)
    {
        currHealth = hp;
    }

    public float GetCurrStamina()
    {
        return currStamina;
    }

    public void SetCurrStamina(float s)
    {
        currHealth = s;
    }

    public int GetIdx()
    {
        return idx;
    }

    public void SetIdx(int index)
    {
        idx = index;
    }
}


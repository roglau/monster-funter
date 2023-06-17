using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerObject instance = PlayerObject.GetInstance();
    private int ctr = 0;
    public BoxCollider box;

    private void Update()
    {        
        if (!EnemyController.isAttack)
        {
            ctr = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (EnemyController.isAttack)
        {
            ctr++;
            if (other.gameObject.tag == "Player" && ctr == 1)
            {
                instance.SetCurrHp(instance.GetCurrHp() - 10);
                box.enabled = false;
            }
        }
        
    }
}

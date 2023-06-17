using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collided = false;
    public GameObject impact;
    private void OnCollisionEnter(Collision collision)
    {
        if(!collided && collision.gameObject.tag != "Player" && collision.gameObject.tag != "Bullet")
        {
            Debug.Log(collision.gameObject.name);
            if(collision.gameObject.tag == "Minion")
            {
                collision.gameObject.GetComponent<Enemy>().CurrHp -= 10;
            }
            
            collided = true;

            var obj = Instantiate(impact, collision.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(gameObject);
            Destroy(obj, 2f);
        }
    }

    private void SetMinionDamaged()
    {   
        Debug.Log("woi");
        EnemyController.Damaged();
    }
}

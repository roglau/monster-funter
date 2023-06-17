using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed;
    public float walkRadius;
    public Animator animator;
    public BoxCollider dmgBox;

    private PlayerObject instance = PlayerObject.GetInstance();
    private enum EnemyState { Idle, Chase, Attacking, Die };

    

    private static EnemyState currentState = EnemyState.Idle;
    public static bool isAttack = false;
    public Slider health;

    private static Enemy en;

    private System.Random rand = new System.Random();

    private string str = null;

    private void Start()
    {
        isAttack = false;
        currentState = EnemyState.Idle;
        dmgBox.enabled = false;
        //health.value = en.CurrHp;
        en = gameObject.GetComponent<Enemy>();
    }

    private void Update()
    {
        Debug.Log(en.CurrHp +"curhp ni bos");
        //health.value = en.CurrHp;
        dmgBox.enabled = false;
        if(health.value == 0)
        {
            currentState = EnemyState.Die;
        }
        switch (currentState)
        {
            case EnemyState.Idle:
                animator.SetBool("isWalking", false);
                if(str!=null)
                animator.SetBool(str, false);
                
                break;
            case EnemyState.Chase:
                animator.SetBool("isWalking", true);
                agent.SetDestination(instance.P.transform.position);
                var distance = Vector3.Distance(transform.position, instance.P.transform.position);
                if(distance <= 1.6f)
                {
                    animator.SetBool("isWalking", false);
                    currentState = EnemyState.Attacking;
                }
                break;
            case EnemyState.Attacking:
                dmgBox.enabled = true;
                if (!isAttack)
                {
                    isAttack = true;
                    var x = rand.Next(3) + 1;
                    Debug.Log(x);
                    str = "Attack" + x;
                    animator.SetBool(str, true);
                    Invoke("AttackFalse", 1f);
                }
                break;
            case EnemyState.Die:
                if(health.value == 0)
                {
                    animator.SetBool("isWalking", false);
                    if (str != null)
                        animator.SetBool(str, false);
                    animator.SetBool("Death", true);
                }
                else
                {
                    currentState = EnemyState.Idle;
                }
                break;
        }   
    }

    public static void StateDie()
    {
        currentState = EnemyState.Die;
    }

    public static void Damaged()
    {
        //en.CurrHp -= 10;
        en.CurrHp -= 10;
        Debug.Log("Damaged" + en.CurrHp);
    }

    private void AttackFalse()
    {
        animator.SetBool(str, false);
        isAttack = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            currentState = EnemyState.Chase;
            agent.isStopped = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            currentState = EnemyState.Idle;
            agent.isStopped = true;
        }
    }
}

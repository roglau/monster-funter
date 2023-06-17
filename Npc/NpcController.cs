using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour
{
    public NavMeshAgent agent;
    public float speed;
    public float walkRadius;
    public Animator animator;

    private PlayerObject instance = PlayerObject.GetInstance();
    private enum NpcState { Idle, Moving, Interact };
    private static NpcState currentState = NpcState.Idle;
    private bool isWalking = false;
    private float random;
   

    void Start()
    {
        //agent.speed = speed;
        //agent.SetDestination(RandomMeshLocation());
        //SetDestination();
        animator.SetBool("isWalking", false);
        random = Random.Range(20, 61);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("inicok" + currentState);
        //Debug.Log("ini pos" + Vector3.Distance(transform.position, instance.p.transform.position));
        //if (agent.remainingDistance <= agent.stoppingDistance)
        //{
        //    agent.SetDestination(RandomMeshLocation());
        //}
        //Debug.Log(random);
        switch (currentState)
        {
            case NpcState.Idle:
                animator.SetBool("isWalking", false);
                //Debug.Log("sebelum rotuin"+currentState
                //StartCoroutine(MoveTrigger());
                Invoke("StateMove", 4f);
                break;

            case NpcState.Moving:
                animator.SetBool("isWalking", true);
                SetDestination();
                Invoke("DestArrive", 12f);
                break;

            case NpcState.Interact:
                agent.SetDestination(transform.position);
                //Debug.Log("Interactcok");
                animator.SetBool("isWalking", false);
                var lookPos = instance.P.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
                break;
        }
    }

    public void SetDestination(Vector3? target = null)
    {
        if (target == null)
        {
            Vector3 randomPosition = Random.insideUnitSphere * walkRadius;
            NavMesh.SamplePosition(randomPosition + transform.position, out NavMeshHit hit, walkRadius, 1);
            agent.SetDestination(hit.position);
            currentState = NpcState.Moving;
        }
    }

    public Vector3 RandomMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius;

        randomPosition += transform.position;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void StateMove()
    {
        if(currentState != NpcState.Interact)
        currentState = NpcState.Moving;
    }


    public void DestArrive()
    {
        animator.SetBool("isWalking", false);
        agent.SetDestination(transform.position);
        StateIdle();
    }

    public static void StateInteract()
    {
        currentState = NpcState.Interact;
    }

    public static void StateIdle()
    {
        currentState = NpcState.Idle;
    }
}

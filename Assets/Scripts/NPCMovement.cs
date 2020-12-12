using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform[] target;
 //   private NPCMotor npcMotor;
    private NavMeshAgent agent;
    private Animator animator;
    private bool arrived = false;


    int i = 0;
    void Start()
    {
        //npcMotor = transform.GetComponent<NPCMotor>();
        // npcMotor.MoveToPoint(target[i].position);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GoToNext();
    }
    void GoToNext()
    {
        //npcMotor.MoveToPoint(target[i].position);
        animator.SetBool("isWalking", true);
        agent.destination = target[i].position;
        i = i + 1;
        UnityEngine.Debug.Log("i" + i);
       // UnityEngine.Debug.Log(target[i].position);
        if (i == target.Length)
        {
            i = 0;
        }
        arrived = false;
    }
    // Update is called once per frame
    void Update()
    {
        // if (npcMotor.arrived)
        //   GoToNext();

        if (!arrived && agent.remainingDistance <= 0)
        {
            arrived = true;
            animator.SetBool("isWalking", false);
            GoToNext();
        }
    }
}

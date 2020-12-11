using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMotor : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    private Animator animator;
    public bool arrived;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        arrived = false;
    }

    public void MoveToPoint(Vector3 point)
    {
        animator.SetBool("isWalking", true);
        agent.SetDestination(point);
        StartCoroutine(MoveToPointTracker());
        arrived = false;
    }
    public IEnumerator MoveToPointTracker()
    {
        while (true)
        {
            if (!agent.pathPending && agent.enabled)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        break;
                    }
                }
            }
            yield return null;
        }
        animator.SetBool("isWalking", false);
        arrived = true;
    }
}

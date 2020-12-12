using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    [HideInInspector]
    public static PlayerMotor instance;
    public NavMeshAgent agent;
    private Animator animator;
    public bool arrived;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        arrived = false;
    }

    public void MoveToPoint(Vector3 point)
    {
        GameManager.instance.controlsEnabled = false;
        agent.enabled = true;
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", true);
        agent.SetDestination(point);
        StartCoroutine(MoveToPointTracker(point));
        arrived = false;
    }
    public void MoveToTarget(Interactable target)
    {
        GameManager.instance.controlsEnabled = false;
        agent.enabled = true;
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", true);
        //agent.stoppingDistance = target.radius;
        agent.SetDestination(target.interactionTransform.position);
        StartCoroutine(MoveToTargetTracker(target));
        arrived = false;
    }
    public IEnumerator FaceTarget(Vector3 target)
    {
        Quaternion initialRotation = transform.rotation;
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        for (float t = 0f; t < 1f; t += 4f * Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, t);
            yield return null;
        }
        direction = (target - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        agent.updateRotation = true;
    }

    public IEnumerator MoveToPointTracker(Vector3 point)
    {
        while (true)
        {
            if (!agent.pathPending && agent.enabled)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        //yield return FaceTarget(point);
                        break;
                    }
                }
            }
            yield return null;
        }
        GameManager.instance.controlsEnabled = true;
        agent.ResetPath();
        agent.enabled = false;
        animator.SetBool("isWalking", false);
        arrived = true;
    }

    public IEnumerator MoveToTargetTracker(Interactable interactable)
    {
        while (true)
        {
            if (!agent.pathPending && agent.enabled)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        yield return FaceTarget(interactable.interactionTransform.position);
                        break;
                    }
                }
            }
            yield return null;
        }
        GameManager.instance.controlsEnabled = true;
        agent.ResetPath();
        agent.enabled = false;
        animator.SetBool("isWalking", false);
        interactable.OnFocused(transform);
        arrived = true;
    }
}

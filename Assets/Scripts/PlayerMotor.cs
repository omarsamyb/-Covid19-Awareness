using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    [HideInInspector]
    public static PlayerMotor instance;
    public NavMeshAgent agent;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
    public void MoveToTarget(Interactable target)
    {
        //agent.updateRotation = false;
        agent.stoppingDistance = target.radius;
        agent.SetDestination(target.interactionTransform.position);
        //StartCoroutine(FaceTarget(target.interactionTransform));
    }
    public IEnumerator FaceTarget(Transform target)
    {
        Quaternion initialRotation = transform.rotation;
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        for (float t = 0f; t < 1f; t += 4f * Time.deltaTime)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, lookRotation, t);
            yield return null;
        }
        direction = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        agent.updateRotation = true;
    }
}

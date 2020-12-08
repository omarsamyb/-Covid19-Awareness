﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToPoint(Vector3 point)
    {
        agent.SetDestination(point);
    }
    public void MoveToTarget(Transform target)
    {
        //agent.updateRotation = false;
        agent.SetDestination(target.position);
        //FaceTarget(target);
    }
    void FaceTarget(Transform target)
    {
        // Need to edit it to make the player face the objects face.
        Vector3 direction = (target.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f);
    }
}

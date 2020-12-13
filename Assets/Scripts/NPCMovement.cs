using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCMovement : MonoBehaviour
{
    public Transform[] target;
    private NPCMotor npcMotor;

    int i = 0;
    void Start()
    {
        npcMotor = transform.GetComponent<NPCMotor>();
    }
    void GoToNext()
    {
        npcMotor = transform.GetComponent<NPCMotor>();
        npcMotor.MoveToPoint(target[i].position);
        i = (i + 1) % target.Length;
    }
    // Update is called once per frame
    void Update()
    {
        if (i == 0)
        {
            npcMotor.MoveToPoint(target[i].position);
            i = i + 1 % target.Length;
        }

        if (npcMotor.arrived)
        {
            GoToNext();
        }
    }
}

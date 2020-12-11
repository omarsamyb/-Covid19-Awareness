using System.Collections;
using UnityEngine;

public class DoorInteraction : Interactable
{
    private Transform leftDoor;
    private Transform rightDoor;
    private Vector3 leftDoorInitialRotation;
    private Vector3 rightDoorInitialRotation;
    private Vector3 leftDoorCurrentRotation;
    private Vector3 rightDoorCurrentRotation;
    private Vector3 leftDoorToRotation;
    private Vector3 rightDoorToRotation;
    public Transform enterPoint;
    public Transform exitPoint;
    private Animator animator;
    private float speed;
    public override void Interact()
    {
        base.Interact();
        GameManager.instance.controlsEnabled = false;
        speed = 1f;
        leftDoor = transform.GetChild(0);
        rightDoor = transform.GetChild(1);
        leftDoorInitialRotation = leftDoor.localEulerAngles;
        rightDoorInitialRotation = rightDoor.localEulerAngles;
        leftDoorCurrentRotation = leftDoor.localEulerAngles;
        rightDoorCurrentRotation = rightDoor.localEulerAngles;
        leftDoorToRotation = leftDoorCurrentRotation;
        rightDoorToRotation = rightDoorCurrentRotation;
        if (GameManager.instance.isPlayerInsideOffice)
        {
            leftDoorToRotation.y += 100f;
            rightDoorToRotation.y -= 100f;
        }
        else
        {
            leftDoorToRotation.y -= 100f;
            rightDoorToRotation.y += 100f;
        }
        animator = player.GetComponent<Animator>();
        StartCoroutine(CloseDoor());
    }
    IEnumerator OpenDoor()
    {
        Vector3 leftDoorStart = leftDoorCurrentRotation;
        Vector3 rightDoorStart = rightDoorCurrentRotation;
        for (float t = 0; t < 1f; t += speed * Time.deltaTime)
        {
            leftDoorCurrentRotation = Vector3.Lerp(leftDoorStart, leftDoorToRotation, t);
            leftDoor.localEulerAngles = leftDoorCurrentRotation;
            rightDoorCurrentRotation = Vector3.Lerp(rightDoorStart, rightDoorToRotation, t);
            rightDoor.localEulerAngles = rightDoorCurrentRotation;
            yield return null;
        }
    }
    IEnumerator Pass()
    {
        yield return OpenDoor();
        if (GameManager.instance.isPlayerInsideOffice)
        {
            PlayerMotor.instance.agent.enabled = true;
            animator.SetBool("isWalking", true);
            PlayerMotor.instance.MoveToPoint(exitPoint.position);
            GameManager.instance.isPlayerInsideOffice = false;
        }
        else
        {
            PlayerMotor.instance.agent.enabled = true;
            animator.SetBool("isWalking", true);
            PlayerMotor.instance.MoveToPoint(enterPoint.position);
            GameManager.instance.isPlayerInsideOffice = true;
        }
        while (true)
        {
            if (!PlayerMotor.instance.agent.pathPending)
            {
                if (PlayerMotor.instance.agent.remainingDistance <= PlayerMotor.instance.agent.stoppingDistance)
                {
                    if (!PlayerMotor.instance.agent.hasPath || PlayerMotor.instance.agent.velocity.sqrMagnitude == 0f)
                    {
                        break;
                    }
                }
            }
            yield return null;
        }
        PlayerMotor.instance.agent.ResetPath();
        PlayerMotor.instance.agent.enabled = false;
        animator.SetBool("isWalking", false);
        leftDoorToRotation = leftDoorCurrentRotation;
        rightDoorToRotation = rightDoorCurrentRotation;
        if (GameManager.instance.isPlayerInsideOffice)
        {
            leftDoorToRotation.y += 100f;
            rightDoorToRotation.y -= 100f;
        }
        else
        {
            leftDoorToRotation.y -= 100f;
            rightDoorToRotation.y += 100f;
        }
    }
    IEnumerator CloseDoor()
    {
        yield return Pass();
        Vector3 leftDoorStart = leftDoorCurrentRotation;
        Vector3 rightDoorStart = rightDoorCurrentRotation;
        for (float t = 0; t < 1f; t += speed * Time.deltaTime)
        {
            leftDoorCurrentRotation = Vector3.Lerp(leftDoorStart, leftDoorToRotation, t);
            leftDoor.localEulerAngles = leftDoorCurrentRotation;
            rightDoorCurrentRotation = Vector3.Lerp(rightDoorStart, rightDoorToRotation, t);
            rightDoor.localEulerAngles = rightDoorCurrentRotation;
            yield return null;
        }
        leftDoor.localEulerAngles = leftDoorInitialRotation;
        rightDoor.localEulerAngles = rightDoorInitialRotation;
        GameManager.instance.controlsEnabled = true;
    }
}

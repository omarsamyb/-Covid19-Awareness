using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    private float horizontal;
    private float vertical;
    private float run;
    private Vector3 targetDirection;
    private Vector3 direction;
    public float walkSpeed;
    public float runSpeed;
    public float turnSmoothTime;
    private float turnSmoothVelocity;
    private float targetAngle;
    private float angle;
    Animator animator;

    void Start()
    {
        walkSpeed = 2f;
        runSpeed = 4f;
        turnSmoothTime = 0.1f;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        run = Input.GetAxisRaw("Run");
    }

    private void FixedUpdate()
    {
        // Normalized so that we don't move faster when we move in 2 directions
        targetDirection = new Vector3(horizontal, 0f, vertical).normalized;
        if (targetDirection.magnitude != 0)
        {
            // Rotation starts at 0 when pointing forward(z) & increases clockwise
            targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            if(animator.GetBool("isWalking") && !animator.GetBool("isRunning"))
                controller.Move(direction.normalized * Time.fixedDeltaTime * walkSpeed);
            else
                controller.Move(direction.normalized * Time.fixedDeltaTime * runSpeed);

            animator.SetBool("isWalking", true);
            if (run == 1)
                animator.SetBool("isRunning", true);
            else
                animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }
}

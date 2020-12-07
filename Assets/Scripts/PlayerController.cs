using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Camera cam;
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
    private Animator animator;
    private RaycastHit hit;
    private float raycastRange;
    private int worldMask;
    private PlayerMotor motor;
    private bool isAgent;

    void Start()
    {
        cam = Camera.main;
        walkSpeed = 2f;
        runSpeed = 4f;
        turnSmoothTime = 0.1f;
        animator = GetComponent<Animator>();
        raycastRange = 8f;
        worldMask = 1 << 8;
        motor = GetComponent<PlayerMotor>();
        isAgent = false;
        motor.agent.enabled = false;
    }

    void Update()
    {
        // Inputs
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        run = Input.GetAxisRaw("Run");

        // Interactions
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, raycastRange, worldMask))
        {
            if (Input.GetKeyDown(KeyCode.E) && !isAgent)
            {
                isAgent = true;
                motor.agent.enabled = true;
                animator.SetBool("isWalking", true);
                motor.MoveToPoint(hit.point);
            }
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * raycastRange, Color.yellow);
        
    }

    private void FixedUpdate()
    {
        if (!motor.agent.pathPending && isAgent)
        {
            if (motor.agent.remainingDistance <= motor.agent.stoppingDistance)
            {
                if (!motor.agent.hasPath || motor.agent.velocity.sqrMagnitude == 0f)
                {
                    motor.agent.ResetPath();
                    motor.agent.enabled = false;
                    isAgent = false;
                    animator.SetBool("isWalking", false);
                }
            }
        }
        if (!isAgent)
        {
            // Normalized so that we don't move faster when we move in 2 directions
            targetDirection = new Vector3(horizontal, 0f, vertical).normalized;
            if (targetDirection.magnitude != 0)
            {
                animator.SetBool("isWalking", true);
                if (run == 1)
                    animator.SetBool("isRunning", true);
                else
                    animator.SetBool("isRunning", false);

                // Rotation starts at 0 when pointing forward(z) & increases clockwise
                targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                if (animator.GetBool("isWalking") && !animator.GetBool("isRunning"))
                    controller.Move(direction.normalized * Time.fixedDeltaTime * walkSpeed);
                else
                    controller.Move(direction.normalized * Time.fixedDeltaTime * runSpeed);
            }
            else
            {
                animator.SetBool("isWalking", false);
                animator.SetBool("isRunning", false);
            }
        }
    }
}

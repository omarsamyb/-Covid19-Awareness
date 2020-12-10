using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    [HideInInspector]
    public static PlayerController instance;
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
    private int interactableMask;
    private PlayerMotor motor;
    Interactable interactable;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        cam = Camera.main;
        walkSpeed = 2f;
        runSpeed = 4f;
        turnSmoothTime = 0.1f;
        animator = GetComponent<Animator>();
        raycastRange = 8f;
        interactableMask = 1 << 9;
        motor = GetComponent<PlayerMotor>();
        motor.agent.enabled = false;
    }

    void Update()
    {
        if (GameManager.instance.controlsEnabled)
        {
            // Inputs
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
            run = Input.GetAxisRaw("Run");

            // Interactions
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, raycastRange, interactableMask))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        Focus();
                    }
                }
            }
        }
        else
        {
            horizontal = 0;
            vertical = 0;
            run = 0;
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * raycastRange, Color.yellow);
    }

    private void FixedUpdate()
    {
        if (!motor.agent.pathPending && motor.agent.enabled)
        {
            if (motor.agent.remainingDistance <= motor.agent.stoppingDistance)
            {
                if (!motor.agent.hasPath || motor.agent.velocity.sqrMagnitude == 0f)
                {
                    Interact();
                }
            }
        }
        if (GameManager.instance.controlsEnabled)
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

    void Focus()
    {
        GameManager.instance.controlsEnabled = false;
        motor.agent.enabled = true;
        animator.SetBool("isRunning", false);
        animator.SetBool("isWalking", true);
        motor.MoveToTarget(interactable);
    }

    void Interact()
    {
        GameManager.instance.controlsEnabled = true;
        motor.agent.ResetPath();
        motor.agent.enabled = false;
        animator.SetBool("isWalking", false);
        interactable.OnFocused(transform);
    }
}

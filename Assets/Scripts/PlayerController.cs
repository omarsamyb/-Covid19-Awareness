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
    private PlayerMotor motor;
    Interactable interactable;
    Interactable prev;

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
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, GameManager.instance.raycastRange, GameManager.instance.interactableMask))
            {
                interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    if (prev != interactable)
                    {
                        AudioManager.instance.Play("HoverSFX");
                        prev = interactable;
                        GameManager.instance.crosshairHover.gameObject.SetActive(true);
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        GameManager.instance.crosshairHover.gameObject.SetActive(false);
                        if (interactable.CompareTag("DoubleSidedInteractable"))
                            motor.MoveToDoubleSidedTarget(interactable);
                        else
                            motor.MoveToTarget(interactable);
                    }
                }
            }
            else
            {
                GameManager.instance.crosshairHover.gameObject.SetActive(false);
                prev = null;
            }
        }
        else
        {
            horizontal = 0;
            vertical = 0;
            run = 0;
        }
        Debug.DrawRay(cam.transform.position, cam.transform.forward * GameManager.instance.raycastRange, Color.yellow);
    }

    private void FixedUpdate()
    {
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
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    //Components and Objects
    CharacterController characterController;
    PlayerInput playerInput;
    Animator animator;

    //Input Variables
    Vector2 movementInput;
    Vector3 movementVector;
    Vector3 runVector;
    bool isRunPressed;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float runSpeed = 3f;


    //Gravity Variables
    float groundGravity = -0.5f;
    float gravity = -9.81f;

    //Animations Variables
    bool isWalkPressed;
    int isWalkingHash;
    int isRunningHash;
    bool isWalking;
    bool isRunning;
    bool isJumpAnimating;

    //Jump Variables
    int isJumpingHash;
    bool isJumpPressed;
    bool isJumping;
    float jumpVelocity;
    float maxJumpHeight = 1.5f;
    float maxJumpTime = 0.75f;

    //Rotate Variables
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Getters and Setters
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public float RunSpeed { get => runSpeed; set => runSpeed = value; }


    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");

        playerInput.CharacterController.Move.started += OnMove;
        playerInput.CharacterController.Move.performed += OnMove;
        playerInput.CharacterController.Move.canceled += OnMove;

        playerInput.CharacterController.Run.started += OnRun;
        playerInput.CharacterController.Run.canceled += OnRun;

        playerInput.CharacterController.Jump.started += OnJump;
        playerInput.CharacterController.Jump.canceled += OnJump;

        SetupJumpVariables();
    }

    private void SetupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        jumpVelocity = (2 * maxJumpHeight) / timeToApex;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleRotation();
        HandleAnimation();
        if (isRunPressed) {
            characterController.Move(runVector * Time.deltaTime);
        }
        else {
            characterController.Move(movementVector * Time.deltaTime);
        }
        HandleGravity();
        HandleJump();
    }


    void HandleGravity()
    {
        if (characterController.isGrounded) {
            if (isJumpAnimating) {
                animator.SetBool(isJumpingHash, false);
                isJumpAnimating = false;
            }
            movementVector.y = groundGravity;
            runVector.y = groundGravity;
        }
        else {
            movementVector.y += gravity * Time.deltaTime;
            runVector.y += gravity * Time.deltaTime;
        }
    }

    private void HandleAnimation()
    {
        isWalking = animator.GetBool(isWalkingHash);
        isRunning = animator.GetBool(isRunningHash);

        if (isWalkPressed && !isWalking) {
            animator.SetBool(isWalkingHash, true);
        }
        else if (isWalkPressed && isWalking && isRunPressed && !isRunning) {
            animator.SetBool(isRunningHash, true);
        }
        else if (isWalkPressed && isWalking && !isRunPressed && isRunning) {
            animator.SetBool(isRunningHash, false);
        }
        else if (!isWalkPressed && isWalking) {
            animator.SetBool(isWalkingHash, false);
        }
    }

    void HandleRotation()
    {
        float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    private void HandleJump()
    {
        if (isJumpPressed && !isJumping && characterController.isGrounded) {
            isJumping = true;
            isJumpAnimating = true;
            animator.SetBool(isJumpingHash, true);
            movementVector.y = jumpVelocity;
            runVector.y = jumpVelocity;
        }
        else if (!isJumpPressed && isJumping && characterController.isGrounded) {
            animator.SetBool(isJumpingHash, false);
            isJumping = false;
        }
    }

    void OnMove(InputAction.CallbackContext callback)
    {
        movementInput = callback.ReadValue<Vector2>();
        movementVector = new Vector3(movementInput.x, movementVector.y, movementInput.y);
        movementVector *= moveSpeed;
        runVector = movementVector * runSpeed;
        isWalkPressed = movementInput != Vector2.zero ? true : false;

    }

    void OnRun(InputAction.CallbackContext callback)
    {
        isRunPressed = callback.ReadValueAsButton();
    }

    void OnJump(InputAction.CallbackContext callback)
    {
        isJumpPressed = callback.ReadValueAsButton();
    }

    private void OnEnable()
    {
        playerInput.CharacterController.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterController.Disable();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
        if (rb != null && rb.gameObject.tag == "FallingPlatform")
        {
            Debug.Log(hit.gameObject.name);
            StartCoroutine(WaitForFall(rb));
        }
    }
    IEnumerator WaitForFall(Rigidbody rb)
    {
        yield return new WaitForSeconds(0.5f);
        rb.isKinematic = false;
        rb.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(rb.gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    PlayerInput playerInput;
    Vector2 movementInput;
    Vector3 movementVector;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    float smoothTurningValue = 480f;
    float dashTime = 1f;

    bool dashPressed;
    bool canDash = false;
    bool canMove = true;

    int isWalkingHash;
    int isDashingHash;


    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isDashingHash = Animator.StringToHash("isDashing");

        playerInput = new PlayerInput();
        playerInput.CharacterController.Move.started += OnMove;
        playerInput.CharacterController.Move.performed += OnMove;
        playerInput.CharacterController.Move.canceled += OnMove;

        playerInput.CharacterController.Dash.started += OnDash;
        playerInput.CharacterController.Dash.canceled += OnDash;

    }

    void Start()
    {

    }

    void Update()
    {
        HandleMovement();
        HandleDash();

        rb.MoveRotation(Quaternion.LookRotation(Vector3.LerpUnclamped(transform.forward, movementVector,
            Vector3.Angle(transform.forward, movementVector) / smoothTurningValue)));


    }

    void OnMove(InputAction.CallbackContext callback)
    {
        movementInput = callback.ReadValue<Vector2>();

    }

    void OnDash(InputAction.CallbackContext callback)
    {
        dashPressed = callback.ReadValueAsButton();
    }

    void HandleMovement()
    {
        if (canMove) {
            canDash = true;
            animator.SetBool(isDashingHash, false);
            animator.SetBool(isWalkingHash, rb.velocity != Vector3.zero ? true : false);
            movementVector.z = movementInput.y;
            movementVector.x = movementInput.x;
            rb.velocity = movementVector * moveSpeed * Time.deltaTime;
        }

    }

    void HandleDash()
    {
        if (dashPressed && canDash) {
            canMove = false;
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        if (!canMove) {
            canMove = false;
            canDash = false;
            animator.SetBool(isDashingHash, true);
            rb.AddForce(movementVector * 5f, ForceMode.Impulse);
            yield return new WaitForSeconds(dashTime);
            canMove = true;
        }
    }

    private void OnEnable()
    {
        playerInput.CharacterController.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterController.Disable();
    }

    public void PlayWalkingAnimation()
    {
        animator.SetBool(isWalkingHash, true);
    }

}

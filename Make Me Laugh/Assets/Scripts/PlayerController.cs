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
    float smoothTurningValue = 480f;


    int isWalkingHash;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");

        playerInput = new PlayerInput();
        playerInput.CharacterController.Move.started += OnMove;
        playerInput.CharacterController.Move.performed += OnMove;
        playerInput.CharacterController.Move.canceled += OnMove;

    }

    void Start()
    {

    }

    void Update()
    {
        HandleMovement();
        //Debug.Log(rb.velocity);

        rb.MoveRotation(Quaternion.LookRotation(Vector3.LerpUnclamped(transform.forward, movementVector,
            Vector3.Angle(transform.forward, movementVector) / smoothTurningValue)));


    }

    void OnMove(InputAction.CallbackContext callback)
    {
        movementInput = callback.ReadValue<Vector2>();

    }

    void HandleMovement()
    {
        movementVector.z = movementInput.y;
        movementVector.x = movementInput.x;
        rb.velocity = movementVector * moveSpeed * Time.deltaTime;
        animator.SetBool(isWalkingHash, rb.velocity != Vector3.zero ? true : false);

    }

    private void OnEnable()
    {
        playerInput.CharacterController.Enable();
    }

    private void OnDisable()
    {
        playerInput.CharacterController.Disable();
    }

}

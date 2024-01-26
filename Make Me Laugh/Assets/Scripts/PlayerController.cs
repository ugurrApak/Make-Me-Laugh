using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    PlayerInput playerInput;
    Vector2 movementInput;
    Vector3 movementVector;
    [SerializeField] float moveSpeed = 10f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

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
        Debug.Log(movementVector);

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

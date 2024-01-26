using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    PlayerInput playerInput;
    Vector2 movementInput;
    Vector3 movementVector;
    [SerializeField] float moveSpeed = 10f;


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

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
        characterController.Move(movementVector * moveSpeed * Time.deltaTime);
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

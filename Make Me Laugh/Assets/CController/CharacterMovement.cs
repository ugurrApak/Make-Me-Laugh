using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    CharacterController characterController;
    PlayerInput playerInput;

    Vector2 movementInput;
    Vector3 movementVector;
    [SerializeField] float moveSpeed = 5f;

    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();

        playerInput.CharacterController.Move.started += OnMove;
        playerInput.CharacterController.Move.performed += OnMove;
        playerInput.CharacterController.Move.canceled += OnMove;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }


    void HandleMovement()
    {
        movementVector = new Vector3(movementInput.x, 0f, movementInput.y);
        characterController.Move(movementVector * moveSpeed * Time.deltaTime);
    }

    void OnMove(InputAction.CallbackContext callback)
    {
        movementInput = callback.ReadValue<Vector2>();
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

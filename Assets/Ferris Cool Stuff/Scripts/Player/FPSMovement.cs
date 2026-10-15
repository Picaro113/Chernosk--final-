using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class FPSMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float crouchSpeed = 2f;

    [Header("Jump And Fall")]
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float gravity = -12f;
    [SerializeField] private float initialFallVelocity = -2f;

    [Header("Crouching")]
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchingHeight = 1f;
    [SerializeField] private float crouchTranstionSpeed = 10f;
    [SerializeField] private float cameraOffset = 0.4f;
       

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference crouchAction;
    [SerializeField] private InputActionReference sprintAction;


    private CharacterController characterController;
    private Vector2 moveInput;
    private bool isGrounded;
    private bool isJumping;
    private bool isCrouching;
    private bool isRunning;
    private float verticalVelocity;
    private float targetHeight;

    private void Update()
    {
        isGrounded = characterController.isGrounded;
        HandleGravity();
        HandleMovement();
        HandleCrouchTransition();
    }
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        targetHeight = standingHeight;
    }


    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            verticalVelocity = jumpForce;
        }
    }
    private void HandleMovement()
    {
        var move = cameraTransform.TransformDirection(new Vector3(moveInput.x, 0, moveInput.y)).normalized;
        var currentSpeed = isCrouching ? crouchSpeed : isRunning ? runSpeed : walkSpeed;
        var finalMove = move * currentSpeed;
        finalMove.y = verticalVelocity;

        var collisions = characterController.Move(finalMove * Time.deltaTime);
        if ((collisions & CollisionFlags.Above) != 0)
        {
            verticalVelocity = initialFallVelocity;
        }
    }
    private void HandleGravity()
    {
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = initialFallVelocity;
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    private void StoreMovementInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }


    private void OnEnable()
    {
        moveAction.action.performed += StoreMovementInput;
        moveAction.action.canceled += StoreMovementInput;
        jumpAction.action.performed += Jump;
        sprintAction.action.performed += Sprint;
        sprintAction.action.canceled += Sprint;
        crouchAction.action.performed += Crouch;
    }

    private void OnDisable()
    {
        moveAction.action.performed -= StoreMovementInput;
        moveAction.action.canceled -= StoreMovementInput;
        jumpAction.action.performed -= Jump;
        sprintAction.action.performed -= Sprint;
        sprintAction.action.canceled -= Sprint;
        crouchAction.action.performed -= Crouch;
    }

    private void Crouch(InputAction.CallbackContext context)
    {
        if (isCrouching)
        {
            if (!CanStandUp())
            {
                return;
            }
            targetHeight = standingHeight;
        }
        else
        {
            targetHeight = crouchingHeight;
        }

        isCrouching = !isCrouching;
    }

    private bool CanStandUp()
    {
        return !Physics.CapsuleCast(transform.position + characterController.center, transform.position + (Vector3.up * characterController.height / 2), characterController.radius, Vector3.up);
    }

    private void Sprint(InputAction.CallbackContext context)
    {
        isRunning = context.performed;
    }
    
    private void HandleCrouchTransition()
    {
        var currentHeight = characterController.height;
        if(Mathf.Abs(currentHeight - targetHeight) < 0.01f)
        {
            characterController.height = targetHeight;
            return;
        }

        var newHeight = Mathf.Lerp(currentHeight, targetHeight, crouchTranstionSpeed * Time.deltaTime);
        characterController.height = newHeight;
        characterController.center = Vector3.up * (newHeight * 0.5f);

        var cameraTargetPosition = cameraTransform.localPosition;
        cameraTargetPosition.y = targetHeight - cameraOffset; 
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, cameraTargetPosition, crouchTranstionSpeed * Time.deltaTime);
    }
}

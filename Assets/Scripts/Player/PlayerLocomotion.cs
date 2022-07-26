using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float sprintingSpeed = 5f;
    [SerializeField] private float walkingSpeed = 3f;
    [SerializeField] private float runningSpeed = 3f;
    [SerializeField] private float rotationSpeed = 5f;

    [Header("Gravity")]
    [SerializeField] private float inAirTimer;
    [SerializeField] private float leapingVelocity;
    [SerializeField] private float fallingVelocity;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayCastOriginOffset;
    [SerializeField] private float maxDistance = 1;
    [SerializeField] private float gravityIntensity = -15;
    [SerializeField] private float jumpHeight = 3;



    [SerializeField] private Transform cameraObject;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isWalking;
    public bool isGrounded;
    public bool isJumping;

    Vector3 moveDirection;
    Rigidbody _playerRigidbody;

    private InputManager _input;
    private PlayerAnimatorManager _playerAnimatorManager;
    private PlayerManager _playerManager;

    private void Awake()
    {
        _input = GetComponent<InputManager>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        _playerManager = GetComponent<PlayerManager>();
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (_playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();
        HandleJump();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * _input.verticalInput;
        moveDirection = moveDirection + cameraObject.right * _input.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            if (isWalking)
            {
                moveDirection = moveDirection * walkingSpeed;
            }
            else
            {
                moveDirection = moveDirection * runningSpeed;
            }
        }



        Vector3 movementVelocity = moveDirection;
        _playerRigidbody.velocity = movementVelocity;
        
    }

    private void HandleRotation()
    {
        if (isJumping)
            return;

        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * _input.verticalInput;
        targetDirection = targetDirection + cameraObject.right * _input.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;

        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastOriginOffset;

        if (!isGrounded && !isJumping)
        {
            if (!_playerManager.isInteracting)
            {
                _playerAnimatorManager.PlayTargetAnimation("inAirIdle", true);
            }

            inAirTimer = inAirTimer + Time.deltaTime;
            _playerRigidbody.AddForce(transform.forward * leapingVelocity);
            _playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, maxDistance, groundLayer))
        {
            if (!isGrounded && !_playerManager.isInteracting)
            {
                _playerAnimatorManager.PlayTargetAnimation("Landing", false);
            }

            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }

    public void HandleJump()
    {

        if (isGrounded)
        {
            _playerAnimatorManager._animator.SetBool("isJumping", true);
            _playerAnimatorManager.PlayTargetAnimation("Jump", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            _playerRigidbody.velocity = playerVelocity;
        }

    }

}

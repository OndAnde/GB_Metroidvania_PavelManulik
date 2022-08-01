using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    InputSettings _inputSettings;
    PlayerAnimatorManager _animatorManager;
    PlayerActionManager _actionManager;
    PlayerLocomotion _locomotion;
    GunSystem _gunSystem;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float verticalInput;
    public float horizontalInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;

    public bool sprintInput;
    public bool jumpInput;
    public bool walkInput = false;
    public bool shootImput;
    public bool actionImput;

    private void Awake()
    {
        _animatorManager = GetComponent<PlayerAnimatorManager>();
        _locomotion = GetComponent<PlayerLocomotion>();
        _gunSystem = GetComponentInChildren<GunSystem>();
        _actionManager = GetComponent<PlayerActionManager>();
    }

    private void OnEnable()
    {
        if (_inputSettings == null)
        {
            _inputSettings = new InputSettings();

            _inputSettings.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            _inputSettings.Player.CameraMove.performed += i => cameraInput = i.ReadValue<Vector2>();
            _inputSettings.Player.Sprint.performed += i => sprintInput = true;
            _inputSettings.Player.Sprint.canceled += i => sprintInput = false;
            _inputSettings.Player.Walk.performed += i => walkInput = !walkInput;
            _inputSettings.Player.Jump.performed += i => jumpInput = true;
            _inputSettings.Player.Shoot.performed += i => shootImput = true;
            _inputSettings.Player.Shoot.canceled += i => shootImput = false;
            _inputSettings.Player.Action.performed += i => actionImput = true;
        }

        _inputSettings.Enable();
    }

    private void OnDisable()
    {
        _inputSettings.Disable();
    }

    public void HandleAllInput()
    {
        HandleMovementInput();
        HandleSprintingInput();
        HandleWalkingInput();
        HandleJumpInput();
        HandleShootInput();
        HandleActionInput();

    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        _animatorManager.UpdateAnimatorValues(0, moveAmount, _locomotion.isSprinting, _locomotion.isWalking);
    }

    private void HandleSprintingInput()
    {
        if (sprintInput)
        {
            _locomotion.isSprinting = true;
        }
        else
        {
            _locomotion.isSprinting = false;
        }
    }

    private void HandleWalkingInput()
    {
        if (walkInput)
        {
            _locomotion.isWalking = true;
        }
        else
        {
            _locomotion.isWalking = false;
        }
    }

    private void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            _locomotion.HandleJump();
        }
    }

    private void HandleShootInput()
    {
        if (shootImput)
        {
            if (_gunSystem && _gunSystem.isAutomatic)
            {
                _gunSystem.handleShoot();
            }
            else if (_gunSystem)
            {
                shootImput = false;
                _gunSystem.handleShoot();
            }
        }
    }

    private void HandleActionInput()
    {
        if (actionImput)
        {
            actionImput = false;
            _actionManager.HandleAction();
        }
    }

}

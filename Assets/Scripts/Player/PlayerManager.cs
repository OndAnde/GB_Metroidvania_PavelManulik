using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [SerializeField] private CameraManager _camera;

    Animator _animator;
    InputManager _input;
    PlayerLocomotion _locomotion;

    public bool isInteracting;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponent<InputManager>();
        _locomotion = GetComponent<PlayerLocomotion>();
    }

    private void Update()
    {
        _input.HandleAllInput();
    }

    private void FixedUpdate()
    {
        _locomotion.HandleAllMovement();
    }

    private void LateUpdate()
    {
        _camera.HandleAllCameraMovement();

        isInteracting = _animator.GetBool("isInteracting");
        _locomotion.isJumping = _animator.GetBool("isJumping");
        _animator.SetBool("isGrounded", _locomotion.isGrounded);
    }
}

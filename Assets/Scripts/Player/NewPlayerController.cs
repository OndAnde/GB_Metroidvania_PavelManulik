using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject Camera;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnStep = 1f;
    [SerializeField] private float angularSpeed = .5f;
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;

    [SerializeField] private float inAirTimer;
    [SerializeField] private float jumpPower;
    [SerializeField] private float leapingVelocity;
    [SerializeField] private float fallingVelocity;
    [SerializeField] private float rayCastHeightOffset;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody _characterRigidbody;
    private Animator _characterAnimator;
    private InputSettings _input;
    private bool isRunning;
    private bool isGrounded;
    private Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        _characterRigidbody = GetComponent<Rigidbody>();
        _characterAnimator = GetComponent<Animator>();
    }

    private void Awake()
    {
        _input = new InputSettings();

        _input.Player.Shoot.performed += context => Shoot();

        _input.Player.Jump.performed += context => Jump();
        
        
        _input.Player.Sprint.started += context => isRunning = true;
        _input.Player.Sprint.canceled += context => isRunning = false;

    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        CheckGrounding();
        _characterRigidbody.angularVelocity = Vector3.zero;
        if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            _characterAnimator.SetBool("inAir", false);
            _characterAnimator.SetBool("Landing", true);
            direction = _input.Player.Move.ReadValue<Vector2>();
            MoveController(direction);
            
        }
        else
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1f))
            {
                _characterAnimator.SetBool("Landing", true);
            }
            else
            {
                _characterAnimator.SetBool("Landing", false);
            }

            _characterAnimator.SetBool("Jump", false);
            _characterAnimator.SetBool("inAir", true);

        }


    }

    private void MoveController(Vector2 direction)
    {
        Vector3 p = _characterRigidbody.velocity;
        direction = Vector2.ClampMagnitude(direction, 1);
        Vector3 forward = transform.forward;
        Vector3 sides = transform.right;
        p = forward * direction.y + sides * direction.x;

        _characterAnimator.SetFloat("x", (direction.x == 0 ? 0 : direction.x < 0 ? -1 : 1) * (isRunning ? 8f : 4));
        _characterAnimator.SetFloat("y", (direction.y == 0 ? 0 : direction.y < 0 ? -1 : 1) * (isRunning ? 8f : 4));
        Debug.Log("Vel origin - " + _characterRigidbody.velocity);
        _characterRigidbody.velocity = (isRunning ? runSpeed * speed : speed) * Vector3.ClampMagnitude(p, 1);
        Debug.Log("Vel output - " + _characterRigidbody.velocity);

    }

    private void Jump()
    {
        
        if (isGrounded)
        {
            print("hehe " + Physics.Raycast(transform.position, Vector3.down, 0.1f));
            _characterAnimator.SetBool("Jump", true);
            _characterAnimator.SetBool("Landing", false);
            _characterRigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        }
        else
        {
            print("not hehe " + Physics.Raycast(transform.position, Vector3.down, 0.1f));
        }
        //_characterRigidbody.AddForce(transform.up * jumpPower, ForceMode.Impulse);

    }

    private void CheckGrounding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if (!isGrounded)
        {
            _characterAnimator.SetBool("inAir", true);
            _characterAnimator.SetBool("Landing", false);
            
            //_characterRigidbody.AddForce(transform.forward * leapingVelocity);
            //_characterRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, rayCastHeightOffset, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded)
            {
                _characterAnimator.SetBool("inAir", false);
                _characterAnimator.SetBool("Landing", true);
            }
            inAirTimer = 0;
            isGrounded = true;
            
        }
        else
        {
            isGrounded = false;
            
        }
    }

    private void Shoot()
    {
       
        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}

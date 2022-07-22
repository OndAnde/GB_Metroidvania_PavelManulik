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
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;



    //private CharacterController _characterController;
    private Rigidbody _characterRigidbody;
    private Animator _characterAnimator;
    private InputSettings _input;
    private bool isRunning;
    private Vector2 direction;
    private Vector3 moveDirection;

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
        //print("Raycast - " + Physics.Raycast(transform.position, Vector3.down, 0f));
        //_characterAnimator.SetBool("inAir", _characterController.isGrounded);
        //if (_characterController.isGrounded)
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
          
            direction = _input.Player.Move.ReadValue<Vector2>();
            MoveController(direction);
            //moveDirection.y -= gravity * Time.deltaTime;
            //_characterController.Move(moveDirection);
        }


    }

    private void MoveController(Vector2 direction)
    {
        Vector3 p = _characterRigidbody.velocity;
        Debug.Log("Forward - " + transform.forward);
        Debug.Log("Right - " + transform.right);
        Vector3 s = transform.forward + transform.right;
       
        s.x *= direction.x * speed * Time.deltaTime;
        s.z *= direction.y * speed * Time.deltaTime;

        _characterAnimator.SetFloat("x", direction.x * 1);
        _characterAnimator.SetFloat("y", direction.y * (isRunning ? 7.9f : 4));
        moveDirection = new Vector3(direction.x * speed * Time.deltaTime, 0, direction.y * speed * Time.deltaTime);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1) * speed;
        //_characterRigidbody.velocity = new Vector3(moveDirection.x, _characterRigidbody.velocity.y, moveDirection.z);
        _characterRigidbody.velocity = s;
        //moveDirection.x
        //Camera.transform.position = transform.position;
        //_characterController.Move(moveDirection);
        //Vector3 moveDirection = new Vector3(-direction.x, 0, -direction.y);
        //transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void Jump()
    {
        
        if (Physics.Raycast(transform.position, Vector3.down, 0.1f))
        {
            print("hehe " + Physics.Raycast(transform.position, Vector3.down, 0.1f));
            _characterAnimator.SetBool("Jump", true);
            _characterAnimator.SetBool("Landing", false);
            _characterRigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        else
        {
            print("not hehe " + Physics.Raycast(transform.position, Vector3.down, 0.1f));
        }


    }

    private void Shoot()
    {
       
        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}

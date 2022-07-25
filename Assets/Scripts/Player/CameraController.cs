using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    [SerializeField] private float sensitivityMouse = 5f;
    [SerializeField] private GameObject pawn;
    private InputSettings _input;
    private Vector2 direction;
    private Vector3 moveDirection;
    private Vector3 playerMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        _input = new InputSettings();
        _input.Player.CameraMove.performed += context => MoveController(_input.Player.CameraMove.ReadValue<Vector2>());

    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse0))
        //{
        //    mouseX = Input.GetAxis("Mouse X") * sensitivityMouse * Time.deltaTime;
        //    mouseY = Input.GetAxis("Mouse Y") * sensitivityMouse * Time.deltaTime;
        //}
        //direction = _input.Player.CameraMove.ReadValue<Vector2>();
        
        //MoveController(direction);

        //Player.Rotate(mouseX * new Vector3(0, 1, 0));

        //transform.Rotate(-mouseY * new Vector3(1, 0, 0));

    }

    private void MoveController(Vector2 direction)
    {
        //moveDirection = new Vector3(-direction.x * sensitivityMouse * Time.deltaTime, -direction.y * sensitivityMouse * Time.deltaTime, 0);
        //moveDirection = Vector3.ClampMagnitude(moveDirection, 1) * sensitivityMouse;

        //var rotation = Vector3.RotateTowards(transform.forward, moveDirection, sensitivityMouse * Time.deltaTime, 0f);
        //transform.rotation = Quaternion.LookRotation(rotation);


        playerMoveDirection = new Vector3(direction.x, 0, 0);
        //playerMoveDirection = Vector3.ClampMagnitude(playerMoveDirection, 1);
        print(Vector3.ClampMagnitude(playerMoveDirection, 1).x);
        //Quaternion.Euler(rigidbody.rotation.eulerAngles + new Vector3(0f, rotationSpeed * Input.GetAxis(MouseX), 0f));
        pawn.transform.rotation = Quaternion.Euler(pawn.transform.rotation.eulerAngles + new Vector3(0f, sensitivityMouse * Vector3.ClampMagnitude(playerMoveDirection,1).x * Time.deltaTime, 0f));

        //_characterController.Move(moveDirection);
        //Vector3 moveDirection = new Vector3(-direction.x, 0, -direction.y);
        //transform.position += moveDirection * speed * Time.deltaTime;
    }
}

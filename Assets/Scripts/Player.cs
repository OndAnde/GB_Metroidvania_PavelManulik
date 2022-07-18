using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool floor;
    private Vector3 moveDirection;

    private string v = "Vertical";
    private string h = "Horizontal";


    //[SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private CharacterController playerCharacterController;
    //[SerializeField] private Animator playerAnimator;
    [SerializeField] private float speed = 500;
    [SerializeField] private float runSpeed = 2;
    [SerializeField] private float jumpPower = 100;
    [SerializeField] private float gravityForce = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (playerCharacterController.isGrounded) {

            moveDirection = new Vector3(Input.GetAxis(h), 0.0f, Input.GetAxis(v)) * speed;
            moveDirection = transform.TransformDirection(moveDirection);
            //if (Input.GetKey(KeyCode.W))
            //{
            //    //Vector3 direction = new Vector3((Input.GetKey(KeyCode.LeftShift) ? speed * runSpeed : speed) * Time.deltaTime, 0, 0);
            //    //playerAnimator.SetFloat("Speed", 1);
            //    moveDirection += transform.forward * (Input.GetKey(KeyCode.LeftShift) ? speed * runSpeed : speed);
            //    //transform.localPosition += transform.forward * (Input.GetKey(KeyCode.LeftShift) ? speed * runSpeed : speed) * Time.deltaTime;
            //}
            ////if (Input.GetKeyUp(KeyCode.W))
            ////{
            ////    playerAnimator.SetFloat("Speed", 0);
            ////}

            //if (Input.GetKey(KeyCode.A))
            //{
            //    moveDirection += -transform.right * speed * Time.deltaTime;
            //    //transform.localPosition += -transform.right * speed * Time.deltaTime;
            //}

            //if (Input.GetKey(KeyCode.S))
            //{
            //    moveDirection += -transform.forward * speed * Time.deltaTime;
            //    //transform.localPosition += -transform.forward * speed * Time.deltaTime;
            //}

            //if (Input.GetKey(KeyCode.D))
            //{
            //    moveDirection += transform.right * speed * Time.deltaTime;
            //    //transform.localPosition += transform.right * speed * Time.deltaTime;
            //}

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //playerRigidBody.AddForce(transform.up * jumpPower);
                moveDirection.y += jumpPower;
            }
        }
        else
        {
            moveDirection.y -= gravityForce * Time.deltaTime;
        }

        playerCharacterController.Move(moveDirection * Time.deltaTime);
        
        

    }

    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        floor = true; 
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Floor")
    //    {
    //        floor = false;
    //    }
    //}
}

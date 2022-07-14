using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool floor;

    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float speed = 500;
    [SerializeField] private float runSpeed = 2;
    [SerializeField] private float jumpPower = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.W))
        {
            //Vector3 direction = new Vector3((Input.GetKey(KeyCode.LeftShift) ? speed * runSpeed : speed) * Time.deltaTime, 0, 0);
            playerAnimator.SetFloat("Speed", 1);
            transform.localPosition += transform.forward * (Input.GetKey(KeyCode.LeftShift) ? speed * runSpeed : speed) * Time.deltaTime;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnimator.SetFloat("Speed", 0);
        }

        if (Input.GetKey(KeyCode.A))
        { 
            transform.localPosition += -transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += -transform.forward * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += transform.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidBody.AddForce(transform.up * jumpPower);
        }

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

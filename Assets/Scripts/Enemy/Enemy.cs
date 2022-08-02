using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private bool floor;

    [SerializeField] private Rigidbody enemyRigidBody;
    [SerializeField] private float jumpPower = 100;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (floor == true)
        {
            enemyRigidBody.AddForce(transform.up * jumpPower);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Floor")
        {
            floor = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            floor = false;
        }
    }
}

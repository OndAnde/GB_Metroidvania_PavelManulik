using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private GameObject DroneBody;

    private Transform player;
    public bool targetConfirmed;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerLocomotion>().transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            targetConfirmed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            targetConfirmed = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (targetConfirmed)
        {
            LookAtPlayer();
        }
    }

    private void LookAtPlayer()
    {
        var direction = player.transform.position - transform.position;
        var rotation = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(rotation);
    }



}

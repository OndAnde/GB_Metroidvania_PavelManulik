using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 1f;
    [SerializeField] private GameObject DroneBody;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private Transform[] wayPoints;
    private NavMeshAgent _navAgent;

    private Transform player;
    public bool targetConfirmed;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        
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
        else
        {
            if (_navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                index = (index + 1) % wayPoints.Length;
                _navAgent.SetDestination(wayPoints[index].position);
            }
        }
    }

    private void LookAtPlayer()
    {
        var direction = player.transform.position - transform.position;
        var rotation = Vector3.RotateTowards(transform.forward, direction, rotateSpeed * Time.deltaTime, 0f);
        DroneBody.transform.rotation = Quaternion.LookRotation(rotation);
    }

    private void Init()
    {
        wayPoints[0] = GameObject.Find("DronePoint1").transform;
        wayPoints[1] = GameObject.Find("DronePoint2").transform;
        player = FindObjectOfType<PlayerLocomotion>().transform;
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.SetDestination(wayPoints[0].position);
        
    }


}

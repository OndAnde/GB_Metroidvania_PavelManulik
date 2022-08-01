using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerZone : MonoBehaviour
{
    private Drone _enemy;
    // Start is called before the first frame update
    void Start()
    {
        //var en = GetComponentInChildren<Rigidbody>();
        _enemy = GetComponentInChildren<Drone>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("player");
            _enemy.targetConfirmed = true;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("not player");
            _enemy.targetConfirmed = false;


        }
    }
}

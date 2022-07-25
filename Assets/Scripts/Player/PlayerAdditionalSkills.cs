using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAdditionalSkills : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnStep = 1f;
    [SerializeField] private float angularSpeed = .5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    //    }
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
    //    }
    //}


}

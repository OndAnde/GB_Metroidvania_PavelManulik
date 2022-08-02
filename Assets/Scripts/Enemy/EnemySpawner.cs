using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemy;
    [SerializeField] private int enemyValue;
    [SerializeField] private float spawnStep = 1f;

    private float timeToSpawn;
    private int currentEnemyValue = 0;
    

    // Update is called once per frame
    void Update()
    {
     if (Time.time > timeToSpawn && currentEnemyValue < enemyValue)
        {
            Instantiate(enemy, transform);
            timeToSpawn = Time.time + spawnStep;
            currentEnemyValue++;
        }   
    }

}

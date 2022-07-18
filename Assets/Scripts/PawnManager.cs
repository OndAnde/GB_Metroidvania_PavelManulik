using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float stamina;
    [SerializeField] private GameObject unit;

    //private float currentHealth;

    //private void Awake()
    //{
    //    currentHealth = health;
    //}

    public void DamageDeal(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (unit.name != "PlayerPawn")
            {
                Destroy(unit);
            }
            
            //Time.timeScale = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

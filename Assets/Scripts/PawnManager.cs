using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] private float stamina;
    [SerializeField] private GameObject unit;

    UIManager _uiManager;

    //private float currentHealth;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    public void DamageDeal(float damage)
    {
        health -= damage;
        if (unit.name == "Pawn")
        {
            _uiManager.ShowHealth(health.ToString());
        }
        if (health <= 0)
        {
            if (unit.name != "Pawn")
            {
                Destroy(unit);
            }
            else {
                _uiManager.ShowHealth("DEAD");
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

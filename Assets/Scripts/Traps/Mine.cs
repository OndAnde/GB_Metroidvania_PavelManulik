using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    [SerializeField]  private float kaboom = 500f;
    [SerializeField] private float damage = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Splash(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "PlayerPawn")
        {
            
        }
    }

    private void Splash(GameObject collisionGameObject)
    {
        if(collisionGameObject.TryGetComponent(out Rigidbody rg))
        {
            rg.AddForce(transform.up * kaboom);
        }

        if (collisionGameObject.TryGetComponent(out PawnManager hp))
        {
            hp.DamageDeal(damage);
        }
    }
}

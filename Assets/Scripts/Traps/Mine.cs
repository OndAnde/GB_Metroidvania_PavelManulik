using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{

    [SerializeField]  private float kaboom = 500f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private bool isSplash;
    [SerializeField] private float maxShockwaveRange = 5f;
    [SerializeField] private float shockwaveSpeed = 5f;
    [SerializeField] private SphereCollider mineCollider;

    private float shockwaveRadius = 0f;

    // Start is called before the first frame update

    private void Start()
    {
        shockwaveRadius = mineCollider.radius;
    }

    private void Update()
    {
        if (isSplash)
        {
            CreateShockWave();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("KABOOOM");

        if (collision.gameObject.tag == "Player")
        {
            Splash(collision.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "Pawn")
        {
            
        }
    }

    private void Splash(GameObject collisionGameObject)
    {

        //if(collisionGameObject.TryGetComponent(out Rigidbody rg))
        //{
        //    rg.AddForce(transform.up * kaboom);
        //}
        isSplash = true;

        if (collisionGameObject.TryGetComponent(out PawnManager hp))
        {
            hp.DamageDeal(damage);
        }
    }

    private void CreateShockWave()
    {
        if(mineCollider.radius <= maxShockwaveRange)
        {
            mineCollider.radius += shockwaveSpeed * Time.deltaTime;
        }
        else
        {
            isSplash = false;
            mineCollider.radius = shockwaveRadius;
        }
    }
}

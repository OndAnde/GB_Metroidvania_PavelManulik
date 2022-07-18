using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Hit(collision.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Hit(other.gameObject);
    }

    private void Hit(GameObject collisionGameObject)
    {
        if (collisionGameObject.TryGetComponent(out PawnManager health))
        {
            health.DamageDeal(damage);
        }
        Destroy(gameObject);
    }
}

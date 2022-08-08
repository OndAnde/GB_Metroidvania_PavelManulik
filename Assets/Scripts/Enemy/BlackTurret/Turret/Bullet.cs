using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
    {
        [SerializeField] private float bulletSpeed;
        
        public float damage = 1f;
        private Rigidbody rigidBody;
        private void Start()
        {   
            rigidBody = GetComponent<Rigidbody>();
        }

        public void BulletSetActive(bool state)
        {
            gameObject.SetActive(state);
            if (state)
            {
                StartCoroutine(ExecuteAfterTime(3f, () =>
                {
                    gameObject.SetActive(false);
                }));
            }
            
        }

        IEnumerator ExecuteAfterTime(float time, Action p)
        {
            yield return new WaitForSeconds(time);
            p();
        }

        private void Update()
        {
            rigidBody.angularVelocity = new Vector3(0.01f, 0.01f, 0.01f);
            rigidBody.velocity = transform.forward * bulletSpeed;
        }

        private void OnCollisionEnter(Collision collision)
        {
            DamageDeal(collision.gameObject);
        }

        private void DamageDeal(GameObject collisionGO)
        {
            if (collisionGO.TryGetComponent(out PawnManager health))
            {
                health.DamageDeal(damage);
            }
            BulletSetActive(false);
        }
    }
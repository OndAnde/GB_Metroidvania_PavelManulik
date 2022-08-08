using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform player;

    [SerializeField] private float spawnStep = 1f;
    [SerializeField] private float angularSpeed = .5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private float fireRate = .1f;
    private float nextSpawnTime;

    [SerializeField] private int poolCount = 20;
    private ObjPool<Bullet> pool;
    private int counter = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LookAtPlayer());
    }

    private void OnTriggerExit()
    {
        StopCoroutine(LookAtPlayer());
    }

    private void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        RaycastHit hit;

        while (enabled)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, layerMask))
            {
                if (hit.transform.tag == "Player")
                {
                    if (Time.time > nextSpawnTime)
                    {
                        if (counter >= spawnPoint.Length)
                        {
                            counter = 0;
                        }
                        Bullet bullet = pool.GetFreeElement();
                        bullet.transform.SetPositionAndRotation(spawnPoint[counter].position, spawnPoint[counter].rotation);
                        counter++;
                        nextSpawnTime = Time.time + fireRate;
                    }
                    yield return new WaitForSeconds(spawnStep);
                }
            }
            
        }
        yield return null;
    }

    private void OnDisable()
    {
        StopCoroutine(Shoot());
    }

    private IEnumerator LookAtPlayer()
    {
        while (enabled)
        {
            var direction = player.transform.position - transform.position;
            var rotation = Vector3.RotateTowards(transform.forward, direction, angularSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(rotation);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
        
    }

    private void Init()
    {
        pool = new ObjPool<Bullet>(bulletPrefab, poolCount, transform);
    }
}

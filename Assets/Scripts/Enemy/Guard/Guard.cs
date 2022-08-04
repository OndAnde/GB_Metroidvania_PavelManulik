using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform player;
    [SerializeField] private float spawnStep = 1f;
    [SerializeField] private float angularSpeed = .5f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float fireRate = .1f;
    [SerializeField] private int poolCount = 20;
    [SerializeField] private NavMeshPath path;

    private float nextSpawnTime;
    private ObjPool<Bullet> pool;
    private int counter = 0;
    private NavMeshAgent _navAgent;
    private bool isPlayer;
    private int index;
    // Start is called before the first frame update

    private void Awake()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

        if (isPlayer)
        {
            _navAgent.SetDestination(player.position);
        }
        else
        {
            if (_navAgent.remainingDistance <= _navAgent.stoppingDistance)
            {
                index = (index + 1) % wayPoints.Length;
                _navAgent.SetDestination(wayPoints[index].position);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayer = true;
        //StartCoroutine(LookAtPlayer());
    }

    private void OnTriggerExit()
    {
        isPlayer = false;
        //StopCoroutine(LookAtPlayer());
    }

    //private void OnEnable()
    //{
    //    StartCoroutine(Shoot());
    //}

    //private IEnumerator Shoot()
    //{
    //    RaycastHit hit;

    //    while (enabled)
    //    {
    //        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, layerMask))
    //        {
    //            if (hit.transform.tag == "Player")
    //            {
    //                if (Time.time > nextSpawnTime)
    //                {
    //                    if (counter >= spawnPoint.Length)
    //                    {
    //                        counter = 0;
    //                    }
    //                    Bullet bullet = pool.GetFreeElement();
    //                    bullet.transform.SetPositionAndRotation(spawnPoint[counter].position, spawnPoint[counter].rotation);
    //                    counter++;
    //                    nextSpawnTime = Time.time + fireRate;
    //                }
    //                yield return new WaitForSeconds(spawnStep);
    //            }
    //        }

    //    }
    //    yield return null;
    //}

    //private void OnDisable()
    //{
    //    StopCoroutine(Shoot());
    //}

    //private IEnumerator LookAtPlayer()
    //{
    //    while (enabled)
    //    {
    //        var direction = player.transform.position - transform.position;
    //        var rotation = Vector3.RotateTowards(transform.forward, direction, angularSpeed * Time.deltaTime, 0f);
    //        transform.rotation = Quaternion.LookRotation(rotation);
    //        yield return new WaitForEndOfFrame();
    //    }
    //    yield return null;

    //}

    private void Init()
    {
        pool = new ObjPool<Bullet>(bulletPrefab, poolCount, transform);
        _navAgent = GetComponent<NavMeshAgent>();
        _navAgent.SetDestination(wayPoints[0].position);
    }
}

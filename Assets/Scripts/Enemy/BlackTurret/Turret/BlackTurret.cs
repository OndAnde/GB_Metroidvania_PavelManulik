using UnityEngine;

    public class BlackTurret : MonoBehaviour
    {
        [SerializeField] private float fireRate = .1f;
        [SerializeField] private float rotateSpeed = 1f;
        [SerializeField] private int poolCount = 20;
        [SerializeField] private Bullet bulletPrefab;
        [SerializeField] private GameObject turretTower;
        [SerializeField] private GameObject turretHolder;
        [SerializeField] private Transform[] spawnPoint;

        private float nextSpawnTime;
        private Transform player;
        private bool permissionToShoot = false;
        private ObjPool<Bullet> pool;
        private int counter = 0;

        private void Start()
        {
            Init();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
            {
                permissionToShoot = true;
                player = FindObjectOfType<PlayerLocomotion>().transform;
                turretHolder.GetComponent<Animator>().enabled = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                permissionToShoot = false;
                player = null;
                turretHolder.GetComponent<Animator>().enabled = false;
            }
        }

        private void Update()
        {
            if (permissionToShoot)
            {
                LookAtPlayer();
                Shoot();
            } 
        }

        private void LookAtPlayer()
        {
            var direction = player.transform.position - turretTower.transform.position;
            var rotation = Vector3.RotateTowards(turretTower.transform.forward, direction, rotateSpeed * Time.deltaTime, 0f);
            turretTower.transform.rotation = Quaternion.LookRotation(rotation);
        }

        private void Shoot()
        {
            if (Time.time > nextSpawnTime)
            {
                if (counter >= spawnPoint.Length)
                {
                    counter = 0;
                }
                Bullet bullet = pool.GetFreeElement();
                bullet.transform.position = spawnPoint[counter].position;
                bullet.transform.rotation = spawnPoint[counter].rotation;
                counter++;
                nextSpawnTime = Time.time + fireRate;
            }
        }

        private void Init()
        {
            pool = new ObjPool<Bullet>(bulletPrefab, poolCount, transform);
        }
    }


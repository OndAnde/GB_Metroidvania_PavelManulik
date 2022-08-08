using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private float fireRate;
    [SerializeField] private float spread;
    [SerializeField] private float reloadTime;
    [SerializeField] private int magazineSize;
    [SerializeField] private Transform Spawn;
    //[SerializeField] private GameObject bullet;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Camera camera;
    [SerializeField] private int poolCount = 20;
    private ObjPool<Bullet> pool;

    public bool isAutomatic;
    public bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleShoot()
    {
        Bullet bullet = pool.GetFreeElement();
        bullet.transform.position = Spawn.position;

        bullet.transform.rotation = camera.transform.rotation;
        

    }

    public void Reload()
    {

    }

    private void Init()
    {
        pool = new ObjPool<Bullet>(bulletPrefab, poolCount, transform);
    }
}

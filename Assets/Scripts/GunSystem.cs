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

    public bool isAutomatic;
    public bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void handleShoot()
    {

    }

    public void Reload()
    {

    }
}

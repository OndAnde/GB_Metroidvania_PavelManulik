using UnityEngine;
using System.Collections;

public class PoolExample : MonoBehaviour
{
    [SerializeField] private int count = 10;
    [SerializeField] private bool autoExpand = false;
    [SerializeField] private bool startPooling = false;
    [SerializeField] private Bullet prefab;

    private ObjPool<Bullet> pool;
    // Use this for initialization
    void Start()
    {
        this.pool = new ObjPool<Bullet>(this.prefab, this.count, this.transform);
        this.pool.autoExpand = this.autoExpand;
    }

    // Update is called once per frame
    void Update()
    {
        if (startPooling)
        {
            createObject();
        }
    }

    void createObject()
    {
        var x = Random.Range(-5f, 5f);
        var z = Random.Range(-5f, 5f);
        var y = 0f;
        var pos = new Vector3(x, y, z);
        var obj = this.pool.GetFreeElement();
        obj.transform.position = pos;
    }
}

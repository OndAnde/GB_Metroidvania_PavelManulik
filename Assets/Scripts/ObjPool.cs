using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjPool<T> where T : MonoBehaviour
{

    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }

    private List<T> pool;

    // Use this for initialization
    public ObjPool(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;
    }

    public ObjPool(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;
        this.CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();

        for (int i = 0; i < count; i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDeafault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.SetActive(isActiveByDeafault);
        this.pool.Add(createdObject);
        return createdObject;
    }

    //Use this to configurate yout pool
    public bool HasFreeElement(out T element)
    {
        foreach(var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return true;
            }
        }
        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if(this.HasFreeElement(out var element))
        {
            return element;
        }
        if (this.autoExpand)
        {
            return this.CreateObject(true);
        }
        throw new Exception($"there is no free elements in pool of type {typeof(T)}");
    }
}

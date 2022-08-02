using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{

    public string name;
    public string description;

    public virtual void UseItem()
    {
        Debug.Log(name + "was used");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private Weapon[] inventory;
    private UIManager _uiManager;

    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
    }

    void Start()
    {
        InitVariables();  
    }

    private void InitVariables()
    {
        inventory = new Weapon[3];
    }

    public void AddItem(Weapon newItem)
    {
        int newItemIndex = (int)newItem.weaponStyle;
        if (inventory[newItemIndex] != null)
        {
            RemoveItem(newItemIndex);
        }

        inventory[newItemIndex] = newItem;
    }

    public void RemoveItem(int index)
    {
        inventory[index] = null;
    }

    public Weapon GetItem(int index)
    {
        return inventory[index];
    }
}

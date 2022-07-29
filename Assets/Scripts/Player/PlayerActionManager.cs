using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask layer;
    [SerializeField] Transform cameraPivot;

    InventoryManager _inventoryManager;
    Door _door;

    private void Awake()
    {
        _inventoryManager = GetComponent<InventoryManager>();
    }

    public void HandleAction()
    {
        print("meh");
        RaycastHit hit;

        if (Physics.SphereCast(cameraPivot.position, 0.2f, cameraPivot.forward, out hit, range, layer))
        {
            print("HIT " + hit.transform.tag);
            if(hit.transform.gameObject.layer == 7)
            {
                switch (hit.transform.tag)
                {
                    case "Weapon":
                        Weapon newWeapon = hit.transform.GetComponent<ItemObject>().item as Weapon;
                        _inventoryManager.AddItem(newWeapon);
                        Destroy(hit.transform.gameObject);
                        break;
                    case "Item":
                        break;
                }
            }
            else if(hit.transform.gameObject.layer == 8)
            {
                switch (hit.transform.tag)
                {
                    case "Door":
                        _door = hit.transform.GetComponent<Door>();
                        _door.HandleInteraction();
                        break;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask layer;
    [SerializeField] Transform cameraPivot;

    public void HandleAction()
    {
        RaycastHit hit;

        if (Physics.Raycast(cameraPivot.position, cameraPivot.forward, out hit, range, layer))
        {
            if(hit.transform.gameObject.layer == 7)
            {

            }
            else if(hit.transform.gameObject.layer == 8)
            {

            }
        }
    }
}

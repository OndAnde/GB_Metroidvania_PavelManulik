using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private bool needKey;
    [SerializeField] private bool isOpened = false;
    [SerializeField]  private LayerMask player;
    [SerializeField]  Animator _animator;

    public bool isAutomatic;

    public void HandleInteraction()
    {
        if (isOpened)
        {

                print("player");
                HandleCloseAction();
            
        }
        else
        {
            print("player");
            HandleOpenAction();
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAutomatic)
        {
            if (other.CompareTag("Player"))
            {
                print("player");
                HandleOpenAction();
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (isAutomatic)
        {
            if (other.CompareTag("Player"))
            {
                print("player");
                HandleCloseAction();
            }
        }

    }

    private void HandleOpenAction()
    {
        
        _animator.SetBool("character_nearby", true);
        isOpened = true;

    }

    private void HandleCloseAction()
    {

        _animator.SetBool("character_nearby", false);
        isOpened = false;
    }
}

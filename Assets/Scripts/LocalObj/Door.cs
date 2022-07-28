using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private bool needKey;
    [SerializeField]  private LayerMask player;
    [SerializeField]  Animator _animator;

    public bool isAutomatic;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
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
        else
        {
            if (needKey)
            {

            }
            else
            {

            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (isAutomatic)
        {
            if (other.CompareTag("Player"))
            {
                print("not_player");
                HandleCloseAction();
            }
        }
        else
        {
            if (needKey)
            {

            }
            else
            {

            }
        }

    }

    private void HandleOpenAction()
    {
        

               _animator.SetBool("character_nearby", true);

    }

    private void HandleCloseAction()
    {

                _animator.SetBool("character_nearby", false);

    }
}

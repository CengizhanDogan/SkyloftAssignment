using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.Interact(this);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IExitable exitable = other.GetComponent<IExitable>();

        if (exitable != null)
        {
            exitable.Exit();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            interactable.Interact(this);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        IExitable exitable = collision.gameObject.GetComponent<IExitable>();

        if (exitable != null)
        {
            exitable.Exit();
        }
    }
}

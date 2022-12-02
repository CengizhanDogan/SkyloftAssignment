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
            Interactor interactor = this;
            interactable.Interact(ref interactor);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();

        if (interactable != null)
        {
            Interactor interactor = this;
            interactable.Interact(ref interactor);
        }
    }
}

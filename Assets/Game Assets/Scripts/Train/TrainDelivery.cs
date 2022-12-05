using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainDelivery : MonoBehaviour, IInteractable
{
    [SerializeField] private StackManager stackManager;
    public void Interact(Interactor interactor)
    {
        if (!interactor.TryGetComponent(out Train train)) return;

        train.StackManager.TransferMetal(stackManager);
    }
}

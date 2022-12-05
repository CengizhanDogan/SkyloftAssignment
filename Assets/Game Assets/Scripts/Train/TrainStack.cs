using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainStack : MonoBehaviour, IInteractable
{
    [SerializeField] private StackManager stackManager;
    public void Interact(Interactor interactor)
    {
        if (!interactor.TryGetComponent(out Character character)) return;

        stackManager.TransferMetal(character.StackManager);
    }
}

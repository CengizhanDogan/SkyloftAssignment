using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour, IInteractable
{
    private SpawnPoint spawnPoint;

    public void SetSpawnPoint(SpawnPoint spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }
    public SpawnPoint GetSpawnPoint()
    {
        return spawnPoint;
    }
    public void Interact<T>(ref T interactor)
    {
        StackManager stackManager = interactor as StackManager;
        stackManager.CollectMetal(this);
        spawnPoint.SetMetal(null);
    }
}

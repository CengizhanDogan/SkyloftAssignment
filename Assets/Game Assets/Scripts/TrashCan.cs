using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour, IUnlockable
{
    //Created for testing. You can spend your collected metals here.
    [SerializeField] private int cost = 999;
    public int GetCost()
    {
        return cost;
    }

    public void GetUnlocked() { }
}

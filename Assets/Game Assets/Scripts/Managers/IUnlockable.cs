using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnlockable 
{
    public int GetCost();
    public void GetUnlocked();
}

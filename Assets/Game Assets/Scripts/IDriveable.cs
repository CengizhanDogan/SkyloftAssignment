using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDriveable
{
    void SetDriver(Character character);
    void DriveSeat(out Transform driveSeat);
    void GetStackManager(out StackManager stackManager);
}

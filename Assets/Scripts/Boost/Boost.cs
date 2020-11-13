using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boost : MonoBehaviour
{
    public Item BoostItem;

    // How many times boost can be applied per level
    public int MaxUsePerLevel = -1;

    public abstract void Apply();
    /* {
        BoostManager.Instance.BoostIsRunning = true;
        Do specific behaviour for that booster
        BoostManager.Instance.BoostIsRunning = false;
    }
    */



}

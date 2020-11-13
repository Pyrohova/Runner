using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBoost : Boost
{
    public override void Apply()
    {
        GameManager.Instance.AddLive();
    }

    private void Awake()
    {
        BoostItem = Item.LiveBooster;
        MaxUsePerLevel = -1;
    }
}

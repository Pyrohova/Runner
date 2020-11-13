using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationBoosterItem : ShopItem
{
    void Awake()
    {
        Item = Item.AccelerationBooster;
        Name = "Starter acceleration";
        Price = 60;
        Amount = PlayerDataHolder.GetItemAmount(Item.AccelerationBooster);
    }

}

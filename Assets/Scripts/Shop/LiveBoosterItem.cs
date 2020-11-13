using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBoosterItem : ShopItem
{
    private void Awake()
    {
        Item = Item.LiveBooster;
        Name = "Live";
        Price = 40;
        Amount = PlayerDataHolder.GetItemAmount(Item.LiveBooster) + PlayerDataHolder.GetLives();
    }

    public override void UpdateAmountValue()
    {
        Amount = PlayerDataHolder.GetItemAmount(Item.LiveBooster) + PlayerDataHolder.GetLives();
    }
}

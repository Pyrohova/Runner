using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShopItem : MonoBehaviour
{
    public Item Item;
    public string Name = "";
    public Sprite Icon;
    public int Price = 0;
    public int Amount = 0;

    public virtual void UpdateAmountValue()
    {
        Amount = PlayerDataHolder.GetItemAmount(Item);
    }
}

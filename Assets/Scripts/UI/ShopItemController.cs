using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemController : MonoBehaviour
{
    [SerializeField]
    Image Icon;

    [SerializeField]
    Text Name;

    [SerializeField]
    Text Price;

    [SerializeField]
    Text Amount;

    public Button button;

    public void UpdateInfo(ShopItem item)
    {
        Icon.sprite = item.Icon;
        Name.text = item.Name;
        Price.text = item.Price.ToString();
        Amount.text = item.Amount.ToString();
    } 

}

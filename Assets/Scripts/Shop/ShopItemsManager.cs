using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemsManager : MonoBehaviour
{
    public static ShopItemsManager Instance { get; private set; }

    public IEnumerable<ShopItem> shopItems;

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void InitializeManager()
    {
        shopItems = GetComponents<ShopItem>();
        UpdateItemsInfo();
    }

    public void UpdateItemsInfo()
    {
        foreach (ShopItem item in shopItems)
        {
            item.UpdateAmountValue();
        }
    }

    void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }

}
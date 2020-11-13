using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuController : MonoBehaviour
{
    //Shop UI object
    [SerializeField]
    GameObject shopMenu;

    //Prefab of shop item sector
    [SerializeField]
    GameObject shopItemPrefab;

    //Start point tospawn for shopItemPrefab elements
    [SerializeField]
    GameObject titleSector;

    //How much currency left
    [SerializeField]
    Text coinsBalance;

    //Parent of sectors
    [SerializeField]
    GameObject itemsPanel;

    //Gameobject that has a list of all items that can be bought in the shop
    [SerializeField]
    GameObject shopItemsList;

    [SerializeField]
    Button exitButton;

    // All available items in shop with additional info about them (price, amount, etc)
    private IEnumerable<ShopItem> items;

    //All available items in shop + sectors that are already exist
    private Dictionary<Item, GameObject> spawnedItems;

    private bool CanBuyItem(ShopItem item)
    {
        if (item.Price <= PlayerDataHolder.GetCoins())
            return true;
        return false;
    }

    private void GenerateShopItems()
    {
        float scaleFactor = FindObjectOfType<Canvas>().scaleFactor;

        // Define sectors' positions
        Vector3 startPosition = titleSector.transform.position;
        float distance = titleSector.GetComponent<RectTransform>().rect.height * scaleFactor;
        int counter = 1;

        foreach (ShopItem newItem in items)
        {
            // Create new sectors 
            Vector3 newPosition = new Vector3(startPosition.x, startPosition.y - (distance * counter), startPosition.z);
            GameObject shopItem = Instantiate(shopItemPrefab, newPosition, shopItemPrefab.transform.rotation, itemsPanel.transform);
            //shopItem.transform.SetParent(itemsPanel.transform);

            // Fill fields and attach to button
            shopItem.GetComponent<ShopItemController>().UpdateInfo(newItem);
            shopItem.GetComponent<ShopItemController>().button.onClick.AddListener(() => Buy(newItem));

            spawnedItems.Add(newItem.Item, shopItem);
            ++counter;
        }
    }

    private void Buy(ShopItem selectedItem)
    {
        if (CanBuyItem(selectedItem))
        {
            // Put item into holder
            PlayerDataHolder.IncrementItem(selectedItem.Item);

            // Update info about item in shop
            UpdateItemAmountInfo(selectedItem);

            // Get and save new balance
            PlayerDataHolder.SetCoins(PlayerDataHolder.GetCoins() - selectedItem.Price);
            UpdateBalance();
        }
    }

    private void SetItemsInShop()
    {
        items = ShopItemsManager.Instance.shopItems;
    }

    public void Open()
    {
        shopMenu.SetActive(true);

        ShopItemsManager.Instance.UpdateItemsInfo();
        UpdateAllItemsAmountInfo();
        UpdateBalance();
    }

    public void Close()
    {
        shopMenu.SetActive(false);
    }

    public void UpdateBalance()
    {
        coinsBalance.text = PlayerDataHolder.GetCoins().ToString();
    }

    private void UpdateItemAmountInfo(ShopItem selectedItem)
    {
        selectedItem.UpdateAmountValue();
        spawnedItems[selectedItem.Item].GetComponent<ShopItemController>().UpdateInfo(selectedItem);
    }

    private void UpdateAllItemsAmountInfo()
    {
        foreach (ShopItem item in items)
        {
            UpdateItemAmountInfo(item);
        }
    }

    private void Awake()
    {
        spawnedItems = new Dictionary<Item, GameObject>();
        SetItemsInShop();
        GenerateShopItems();

        exitButton.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowMainMenu();
            Close();
        });
    }
}


using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InventoryModel
{
    private Item itemCardPrefab;
    private List<Item> spawnedItemCardsList;

    private List<ItemData> allGameItems;

    private int maxInventoryWeight;
    private int currentInventoryWeight;
    private int itemsAddedToinventory = 0;
    private int maxItemsSpawnCount = 3;

    public InventoryModel(Item _itemCardPrfab, List<ItemData> _allGameItems, int _maxInventoryWeight)
    {
        itemCardPrefab = _itemCardPrfab;
        maxInventoryWeight = _maxInventoryWeight;
        allGameItems = _allGameItems;

        spawnedItemCardsList = new List<Item>();
        currentInventoryWeight = 0;
    }

    public bool CanAddItem() => currentInventoryWeight < maxInventoryWeight;

    public int GetCurrentInventoryWeight() => currentInventoryWeight;
    public int GetAllGameItemsCount() => allGameItems.Count;
    public int CreateItemId() => (itemsAddedToinventory++);
    public int GetMaximumItemSpawnCount() => maxItemsSpawnCount;

    public ItemData GetFirstItemInInventory() => spawnedItemCardsList[0].GetComponent<Item>().currentItemData;
    public ItemData GetFirstItemInInventoryID() => spawnedItemCardsList[0].currentItemData;
    public ItemData GetItemAtIndex(int index) => allGameItems[index];

    public Item GetInventoryItemCard() => itemCardPrefab;
    public List<Item> GetInventoryItemsList() => spawnedItemCardsList;

    //Weight Manipulation
    public void AddInventoryWeight(int weight) => currentInventoryWeight += weight;
    public void DecreaseInventoryWeight(int weight) => currentInventoryWeight -= weight;
    public void AddItemToInventory(Item _item) => spawnedItemCardsList.Add(_item);
    public void RemoveItemFromInventory(Item _item) => spawnedItemCardsList.Remove(_item);
}

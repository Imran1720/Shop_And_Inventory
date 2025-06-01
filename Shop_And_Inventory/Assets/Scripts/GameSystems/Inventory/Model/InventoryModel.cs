
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InventoryModel
{
    private GameObject itemCardPrefab;
    private List<ItemData> allGameItems;
    private List<GameObject> spawnedItemCardsList;

    private int maxInventoryWeight;
    private int currentInventoryWeight;
    private int itemsAddedToinventory = 0;
    private int maxItemsSpawnCount = 3;

    private InventoryController inventoryController;

    public InventoryModel(GameObject _itemCardPrfab,
        List<ItemData> _allGameItems,
        int _maxInventoryWeight)
    {
        itemCardPrefab = _itemCardPrfab;
        maxInventoryWeight = _maxInventoryWeight;
        allGameItems = _allGameItems;

        spawnedItemCardsList = new List<GameObject>();
        currentInventoryWeight = 0;
    }

    public GameObject GetInventoryItemCard() => itemCardPrefab;
    public int GetCurrentInventoryWeight() => currentInventoryWeight;
    public void InitializeShopController(InventoryController _controller) => inventoryController = _controller;
    public int GetAllGameItemsCount() => allGameItems.Count;
    public int GetRandomSpawnCount() => Random.Range(1, maxItemsSpawnCount + 1);
    public ItemData GetFirstItemInInventory() => spawnedItemCardsList[0].GetComponent<Item>().currentItemData;
    public ItemData GetItemAtIndex(int index) => allGameItems[index];
    public List<GameObject> GetInventoryItemsList() => spawnedItemCardsList;
    public void AddItemToInventory(GameObject _item) => spawnedItemCardsList.Add(_item);
    public void RemoveItemFromInventory(GameObject _item) => spawnedItemCardsList.Remove(_item);
    public bool CanAddItem() => currentInventoryWeight < maxInventoryWeight;
    public int CreateItemId() => (itemsAddedToinventory++);

    //Weight Manipulation
    public void AddInventoryWeight(int weight) => currentInventoryWeight += weight;
    public void DecreaseInventoryWeight(int weight) => currentInventoryWeight -= weight;

}

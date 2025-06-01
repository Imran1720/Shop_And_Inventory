
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InventoryModel
{
    private GameObject itemCardPrefab;
    private InventoryController inventoryController;
    private GameObject itemContainer;

    private List<GameObject> spawnedItemCardsList;
    private int maxInventoryWeight;
    private int currentInventoryWeight;
    private int itemsAddedToinventory = 0;


    private int maxItemsSpawnCount = 3;

    private List<ItemData> allGameItems;
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
    public int GetMaxInventoryWeight() => maxInventoryWeight;
    public int GetCurrentInventoryWeight() => currentInventoryWeight;
    public void AddSpawnedItemCardToList(GameObject _newCard) => spawnedItemCardsList.Add(_newCard);
    public Item GetItemCardAtIndex(int index) => spawnedItemCardsList[index]?.GetComponent<Item>();

    public void ShowItemOfType(ItemType _type)
    {
        if (spawnedItemCardsList == null) return;
        foreach (GameObject _item in spawnedItemCardsList)
        {
            if (_item.GetComponent<Item>().GetItemType() != _type)
            {
                _item.SetActive(false);
            }
            else
            {
                _item.SetActive(true);
            }
        }
    }

    public void ShowAllItems()
    {
        if (spawnedItemCardsList == null) return;
        foreach (GameObject _item in spawnedItemCardsList)
        {
            _item.SetActive(true);
        }
    }

    public void UpdateInventoryWeight(int _value) => currentInventoryWeight += _value;
    public void InitializeShopController(InventoryController _controller) => inventoryController = _controller;
    public int GetAllGameItemsCount() => allGameItems.Count;
    public int GetRandomSpawnCount() => Random.Range(1, maxItemsSpawnCount + 1);
    public GameObject GetShopItemCard() => itemCardPrefab;
    public GameObject GetItemContainer() => itemContainer;
    public ItemData GetFirstItemInInventory() => spawnedItemCardsList[0].GetComponent<Item>().currentItemData;
    public ItemData GetItemAtIndex(int index) => allGameItems[index];
    public List<GameObject> GetInventoryItemsList() => spawnedItemCardsList;
    public void AddItemToInventory(GameObject _item) => spawnedItemCardsList.Add(_item);
    public void RemoveItemFromInventory(GameObject _item) => spawnedItemCardsList.Remove(_item);
    public void RemoveAllItems() => spawnedItemCardsList.Clear();
    public bool CanAddItem() => currentInventoryWeight < maxInventoryWeight;
    public int CreateItemId() => (itemsAddedToinventory++);
    public void AddItemCountWithId(int _id, int _count)
    {
        foreach (GameObject item in spawnedItemCardsList)
        {
            if (item.GetComponent<Item>().currentItemData.id == _id)
            {
                int itemcount = item.GetComponent<Item>().currentItemData.quantity + _count;
                item.GetComponent<Item>().updateItemCount(itemcount);
                return;
            }
        }
    }

    public void AddInventoryWeight(int weight) => currentInventoryWeight += weight;
    public void DecreaseInventoryWeight(int weight) => currentInventoryWeight -= weight;
    public void DecreaseItemCountWithId(int _id, int _count)
    {
        foreach (GameObject item in spawnedItemCardsList)
        {
            if (item.GetComponent<Item>().currentItemData.id == _id)
            {
                int itemcount = (item.GetComponent<Item>().currentItemData.quantity - _count);
                item.GetComponent<Item>().updateItemCount(itemcount);
                return;
            }
        }
    }

    public GameObject GetItemWithId(int _id)
    {
        foreach (GameObject item in spawnedItemCardsList)
        {
            if (item.GetComponent<Item>().currentItemData.id == _id)
            {

                return item;
            }
        }
        return null;
    }

    public void SetItemContainer(GameObject _container) => itemContainer = _container;
}

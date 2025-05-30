
using System.Collections.Generic;
using UnityEngine;

public class InventoryModel
{
    private GameObject itemCardPrefab;
    private InventoryController inventoryController;
    private GameObject itemContainer;

    private List<GameObject> spawnedItemCardsList;
    private int maxInventoryWeight;
    private int currentInventoryWeight;
    private int coins;
    public InventoryModel(GameObject _itemCardPrfab, int _maxInventoryWeight)
    {
        itemCardPrefab = _itemCardPrfab;
        maxInventoryWeight = _maxInventoryWeight;

        spawnedItemCardsList = new List<GameObject>();
        currentInventoryWeight = 10;
        coins = 0;
    }

    public GameObject GetInventoryItemCard() => itemCardPrefab;
    public int GetMaxInventoryWeight() => maxInventoryWeight;
    public int GetCurrentInventoryWeight() => currentInventoryWeight;
    public int GetCurrentMoney() => coins;
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

    public void RemoveItemFromInventory(GameObject _item) => spawnedItemCardsList.Remove(_item);
    public void RemoveAllItems() => spawnedItemCardsList.Clear();
    public List<GameObject> GetInventoryItemsList() => spawnedItemCardsList;
    public void SetItemContainer(GameObject _container) => itemContainer = _container;
    public void InitializeShopController(InventoryController _controller) => inventoryController = _controller;
}

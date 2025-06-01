using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopModel
{
    private GameObject itemCardPrefab;
    private GameObject itemContainer;
    private List<ItemData> allGameItems;
    private List<GameObject> spawnedItemCardsList;

    private ShopController shopController;

    public int defaultItemsSpawnCount;
    private int shopRefreshTime;
    int totalItemsAdded = 0;

    public ShopModel(GameObject _itemCardPrefab,
        List<ItemData> _allGameItems,
         int _defaultItemsSpawnCount,
         int _shopRefreshTime)
    {
        itemCardPrefab = _itemCardPrefab;
        allGameItems = _allGameItems;
        defaultItemsSpawnCount = _defaultItemsSpawnCount;
        shopRefreshTime = _shopRefreshTime;

        spawnedItemCardsList = new List<GameObject>();
    }

    public GameObject GetShopItemCard() => itemCardPrefab;

    public List<ItemData> GetAllGameItems() => allGameItems;

    public void SetSpawnedItemsCardList(List<GameObject> itemsList) => spawnedItemCardsList = itemsList;

    public void InitializeShopController(ShopController _controller) => shopController = _controller;

    public int GetDefaultSpawnCount() => defaultItemsSpawnCount;

    public void AddSpawnedItemCardToList(GameObject _newCard)
    {
        spawnedItemCardsList.Add(_newCard);
    }

    public Item GetItemCardAtIndex(int index) => spawnedItemCardsList[index]?.GetComponent<Item>();

    public int GetAllGameItemsCount() => allGameItems.Count;

    public GameObject GetItemContainer() => itemContainer;
    public ItemData GetGameItemAtIndex(int index) => allGameItems[index];

    public ItemData GetFirstItemInShop() => spawnedItemCardsList[0].GetComponent<Item>().currentItemData;

    public void ShowItemOfType(ItemType _type)
    {
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
    public int GetShopItemCount() => spawnedItemCardsList.Count;

    public void ShowAllItems()
    {
        foreach (GameObject _item in spawnedItemCardsList)
        {
            _item.SetActive(true);
        }
    }

    public List<GameObject> GetShopItemsList() => spawnedItemCardsList;

    public void RemoveItemFromShop(GameObject _item) => spawnedItemCardsList.Remove(_item);
    public void RemoveAllItems() => spawnedItemCardsList.Clear();

    public int GetShopRefreshTime() => shopRefreshTime;
    //Doubt
    public void SetItemContainer(GameObject _container) => itemContainer = _container;
    public int GetTotalItemsAdded() => totalItemsAdded;
    public void IncrementTotalItemCount() => totalItemsAdded++;

}
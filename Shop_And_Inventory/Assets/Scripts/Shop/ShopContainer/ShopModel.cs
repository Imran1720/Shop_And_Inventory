using System.Collections.Generic;
using UnityEngine;

public class ShopModel
{
    private GameObject itemCardPrefab;
    private List<ItemData> allGameItems;
    private List<GameObject> spawnedItemCardsList;

    public int defaultItemsSpawnCount;
    private int shopRefreshTime;
    int totalItemsAdded = 0;

    private ShopController shopController;

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
    public int GetDefaultSpawnCount() => defaultItemsSpawnCount;
    public int GetShopRefreshTime() => shopRefreshTime;
    public int GetTotalItemsAdded() => totalItemsAdded;
    public List<GameObject> GetShopItemsList() => spawnedItemCardsList;
    public List<ItemData> GetAllItems() => allGameItems;

    public void AddSpawnedItemCardToList(GameObject _newCard) => spawnedItemCardsList.Add(_newCard);
    public GameObject GetItemCardAtIndex(int _index) => spawnedItemCardsList[_index];
    public int GetTotalItemsInGame() => allGameItems.Count;
    public ItemData GetGameItemAtIndex(int _index) => allGameItems[_index];
    public GameObject GetFirstItemInShop() => spawnedItemCardsList[0];
    public void RemoveItemFromShop(GameObject _item) => spawnedItemCardsList.Remove(_item);
    public void RemoveAllItems() => spawnedItemCardsList.Clear();
    public void IncrementTotalItemCount() => totalItemsAdded++;
    public void InitializeShopController(ShopController _controller) => shopController = _controller;

}
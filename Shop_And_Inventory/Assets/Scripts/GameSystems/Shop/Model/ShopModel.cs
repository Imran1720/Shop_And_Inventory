using System.Collections.Generic;
using UnityEngine;

public class ShopModel
{
    private Item itemCardPrefab;
    private List<Item> spawnedItemCardsList;

    private List<ItemData> allGameItems;

    private int shopRefreshTime;
    private int totalItemsAdded = 0;
    public int defaultItemsSpawnCount;

    public ShopModel(Item _itemCardPrefab, List<ItemData> _allGameItems, int _defaultItemsSpawnCount, int _shopRefreshTime)
    {
        allGameItems = _allGameItems;
        itemCardPrefab = _itemCardPrefab;
        shopRefreshTime = _shopRefreshTime;
        defaultItemsSpawnCount = _defaultItemsSpawnCount;

        spawnedItemCardsList = new List<Item>();
    }

    public int GetTotalItemsAdded() => totalItemsAdded;
    public int GetShopRefreshTime() => shopRefreshTime;
    public int GetTotalItemsInGame() => allGameItems.Count;
    public int GetDefaultSpawnCount() => defaultItemsSpawnCount;

    public Item GetShopItemCard() => itemCardPrefab;
    public Item GetFirstItemInShop() => spawnedItemCardsList[0];
    public List<Item> GetShopItemsList() => spawnedItemCardsList;
    public Item GetItemCardAtIndex(int _index) => spawnedItemCardsList[_index];

    public ItemData GetGameItemAtIndex(int _index) => allGameItems[_index];

    public void IncrementTotalItemCount() => totalItemsAdded++;
    public void EmptyShop() => spawnedItemCardsList.Clear();
    public void RemoveItemFromList(Item _item) => spawnedItemCardsList.Remove(_item);
    public void AddSpawnedItemCardToList(Item _newItem) => spawnedItemCardsList.Add(_newItem);
}
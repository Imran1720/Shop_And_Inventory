using System.Collections.Generic;
using UnityEngine;

public class ShopModel
{
    private GameObject itemCardPrefab;
    private List<ItemData> allGameItems;
    private List<GameObject> spawnedItemCardsList;

    private ShopController shopController;

    public int defaultItemsSpawnCount;
    private int shopRefreshTime;

    public ShopModel(GameObject _itemCardPrefab,
        List<ItemData> _allGameItems,
         int _defaultItemsSpawnCount,
         int _shopRefreshTime)
    {
        itemCardPrefab = _itemCardPrefab;
        allGameItems = _allGameItems;
        defaultItemsSpawnCount = _defaultItemsSpawnCount;
        shopRefreshTime = _shopRefreshTime;
    }

    public GameObject GetShopItemCard() => itemCardPrefab;

    public List<ItemData> GetAllGameItems() => allGameItems;

    public void SetSpawnedItemsCardList(List<GameObject> itemsList) => spawnedItemCardsList = itemsList;

    public void InitializeShopController(ShopController _controller) => shopController = _controller;

    public int GetDefaultSpawnCount() => defaultItemsSpawnCount;

    public void AddSpawnedItemCardToList(GameObject _newCard) => spawnedItemCardsList.Add(_newCard);
}

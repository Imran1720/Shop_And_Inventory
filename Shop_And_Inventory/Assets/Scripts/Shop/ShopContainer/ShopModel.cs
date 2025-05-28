using System.Collections.Generic;
using UnityEngine;

public class ShopModel
{
    private GameObject itemCardPrefab;
    private List<ItemData> allGameItems;
    private List<GameObject> spawnedItemCardsList;

    private ShopController shopController;

    public int initialShopItemCount;
    private int shopRefreshTime;

    public ShopModel(GameObject _itemCardPrefab,
        List<ItemData> _allGameItems,
         int _initialShopItemCount,
         int _shopRefreshTime)
    {
        itemCardPrefab = _itemCardPrefab;
        allGameItems = _allGameItems;
        initialShopItemCount = _initialShopItemCount;
        shopRefreshTime = _shopRefreshTime;
    }

    public GameObject GetShopItemCard() => itemCardPrefab;

    public List<ItemData> GetAllGameItems() => allGameItems;

    public void SetSpawnedItemsCardList(List<GameObject> itemsList) => spawnedItemCardsList = itemsList;

    public void InitializeShopController(ShopController _controller) => shopController = _controller;

}

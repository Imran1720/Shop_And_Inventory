using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    [Header("Script")]
    private ShopView shopView;

    [Header("Objects")]
    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private GameObject itemCardPrefab;
    [SerializeField] private ItemDataBase itemDatabase;

    [Header("Data")]
    [SerializeField] public int initialShopItemCount;
    [SerializeField] private int shopRefreshTime;

    private List<ItemData> allGameItems;



    private void Start()
    {
        allGameItems = new List<ItemData>();
        LoadAllItemData();

        GameObject shop = Instantiate(shopPrefab) as GameObject;
        shop.transform.SetParent(transform, false);

        shopView = shop.GetComponent<ShopView>();
        ShopModel shopModel = new ShopModel(itemCardPrefab, allGameItems, initialShopItemCount, shopRefreshTime);

        ShopController controller = new ShopController(shopView, shopModel);
    }

    private void LoadAllItemData()
    {

        LoadItemFrom(itemDatabase.materialList);
        LoadItemFrom(itemDatabase.ConsumableList);
        LoadItemFrom(itemDatabase.treasureList);
        LoadItemFrom(itemDatabase.weaponsList);
    }

    private void LoadItemFrom(ItemSO[] _itemsList)
    {
        foreach (ItemSO item in _itemsList)
        {
            allGameItems.Add(CreateItemDataFromSO(item));
        }
    }

    private ItemData CreateItemDataFromSO(ItemSO item)
    {
        ItemData newData = new ItemData();
        newData.id = allGameItems.Count;
        newData.itemClassification = item.itemClassification;
        newData.itemName = item.itemName;
        newData.itemType = item.itemType;
        newData.itemRarity = item.itemRarity;
        newData.icon = item.icon;
        newData.buyingPrice = item.buyingPrice;
        newData.sellingPrice = item.buyingPrice;
        newData.weight = item.weight;
        newData.quantity = item.quantity;
        newData.description = item.description;

        return newData;
    }

}

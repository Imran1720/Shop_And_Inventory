using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{
    [Header("Script")]
    private InventoryView inventoryView;

    [Header("Objects")]
    [SerializeField] private GameObject inventoryPrefab;
    [SerializeField] private GameObject itemCardPrefab;
    [SerializeField] private ItemDataBase itemDatabase;

    [Header("Objects")]
    [SerializeField] private int maxInvnetoryWeight;

    private List<ItemData> allGameItems;
    private void Start()
    {
        allGameItems = new List<ItemData>();
        LoadAllItemData();
        GameObject inventory = Instantiate(inventoryPrefab) as GameObject;
        inventory.transform.SetParent(transform, false);

        inventoryView = inventory.GetComponent<InventoryView>();
        InventoryModel inventoryModel = new InventoryModel(itemCardPrefab, allGameItems, maxInvnetoryWeight);

        InventoryController controller = new InventoryController(inventoryView, inventoryModel);
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

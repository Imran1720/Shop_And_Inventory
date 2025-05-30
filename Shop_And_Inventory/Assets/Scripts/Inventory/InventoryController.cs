using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryController
{
    private InventoryView inventoryView;
    private InventoryModel inventoryModel;

    private float timer;

    public InventoryController(InventoryView _inventoryView, InventoryModel _inventoryModel)
    {
        inventoryModel = _inventoryModel;
        inventoryView = _inventoryView;

        inventoryView.InitializeShopController(this);
        inventoryModel.InitializeShopController(this);
        inventoryModel.SetItemContainer(inventoryView.GetItemContainer());
        inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());

        EventService.Instance.OnItemBought.AddListener(OnItemBought);
    }
    ~InventoryController()
    {
        EventService.Instance.OnItemBought.RemoveListener(OnItemBought);
    }

    public void GatherItems()
    {

        int numberOfCardsToSpawn = inventoryModel.GetRandomSpawnCount();
        for (int i = 0; i < numberOfCardsToSpawn; i++)
        {
            if (!inventoryModel.CanAddItem())
            {
                UIUtility.Instance.ShowInventoryFullNotification();
                return;
            }
            ItemData newDataItem = CreateItemData();
            (bool isItemPresent, int itemId) = IsItemPresentInInventory(newDataItem.itemName);
            if (isItemPresent)
            {
                inventoryModel.UpdateItemCountWithId(itemId, newDataItem.quantity);
            }
            else
            {
                GameObject newItem = CreateItemCards();
                int id = inventoryModel.CreateItemId();
                SetItemData(newItem, id, newDataItem);
                inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());
                inventoryModel.AddItemToInventory(newItem);
            }
        }
        EventService.Instance.OnItemGathered.InvokeEvent(inventoryModel.GetFirstItemInInventory());
    }

    private GameObject CreateItemCards()
    {
        GameObject newItemCard = GameObject.Instantiate(inventoryModel.GetShopItemCard());
        newItemCard.transform.SetParent(inventoryModel.GetItemContainer().transform, false);
        return newItemCard;
    }


    public void SetItemData(GameObject _item, int _id, ItemData _data)
    {
        inventoryModel.UpdateInventoryWeight(_data.weight * _data.quantity);
        _item.GetComponent<Item>().SetItemData(_data, _id, false);

    }

    private ItemData CreateItemData()
    {
        int itemIndex = Random.Range(0, inventoryModel.GetAllGameItemsCount());
        ItemData data = inventoryModel.GetItemAtIndex(itemIndex);
        data.quantity = 1;
        return data;
    }

    public void ShowItemsOfType(ItemType _itemType) => inventoryModel.ShowItemOfType(_itemType);
    public void ShowAllItems() => inventoryModel.ShowAllItems();

    //private void OnBuyingItemFromShop(ItemData _updatedData)
    //{

    //    UpdateItemInList(_updatedData);
    //}

    //private void UpdateItemInList(ItemData data)
    //{
    //    List<GameObject> shopItemsList = shopModel.GetShopItemsList();

    //    foreach (GameObject item in shopItemsList)
    //    {
    //        if (item != null && item.GetComponent<Item>().currentItemData.id == data.id)
    //        {
    //            if (data.quantity <= 0)
    //            {
    //                shopModel.RemoveItemFromShop(item);
    //                GameObject.Destroy(item);
    //            }
    //            else
    //            {
    //                item.GetComponent<Item>().updateItemCount(data.quantity);
    //            }
    //            EventService.Instance.OnShopUpdate.InvokeEvent(shopModel.GetFirstItemInShop());
    //            return;
    //        }
    //    }
    //}

    private void OnItemBought(ItemData _data)
    {
        if (!inventoryModel.CanAddItem())
        {
            UIUtility.Instance.ShowInventoryFullNotification();
            return;
        }
        (bool isItemPresent, int itemId) = IsItemPresentInInventory(_data.itemName);
        if (isItemPresent)
        {
            inventoryModel.UpdateItemCountWithId(itemId, _data.quantity);
        }
        else
        {
            GameObject newItem = CreateItemCards();
            int id = inventoryModel.CreateItemId();
            UpdateItemData(newItem, _data, id);
            inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());
            inventoryModel.AddItemToInventory(newItem);
        }
    }

    public void UpdateItemData(GameObject _item, ItemData _data, int _id)
    {
        inventoryModel.UpdateInventoryWeight(_data.weight * _data.quantity);
        _item.GetComponent<Item>().SetItemData(_data, _id, false);

    }

    private (bool, int) IsItemPresentInInventory(string _name)
    {
        List<GameObject> inventoryItems = inventoryModel.GetInventoryItemsList();
        foreach (GameObject item in inventoryItems)
        {
            if (item.GetComponent<Item>().currentItemData.itemName == _name)
            {
                return (true, item.GetComponent<Item>().currentItemData.id);
            }
        }
        return (false, -1);
    }
}

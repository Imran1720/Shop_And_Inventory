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
        EventService.Instance.OnItemSold.AddListener(OnItemSold);
    }
    ~InventoryController()
    {
        EventService.Instance.OnItemBought.RemoveListener(OnItemBought);
        EventService.Instance.OnItemSold.RemoveListener(OnItemSold);
    }

    public void GatherItems()
    {

        int numberOfCardsToSpawn = inventoryModel.GetRandomSpawnCount();
        for (int i = 0; i < numberOfCardsToSpawn; i++)
        {
            if (!inventoryModel.CanAddItem())
            {
                GameService.instance.UIManager.ShowInventoryFullNotification();
                return;
            }
            ItemData newDataItem = CreateItemData();
            (bool isItemPresent, int itemId) = IsItemPresentInInventory(newDataItem.itemName);
            inventoryModel.AddInventoryWeight(newDataItem.quantity * newDataItem.weight);
            if (isItemPresent)
            {
                inventoryModel.AddItemCountWithId(itemId, newDataItem.quantity);
            }
            else
            {
                GameObject newItem = CreateItemCards();
                int id = inventoryModel.CreateItemId();
                SetItemData(newItem, id, newDataItem);
                inventoryModel.AddItemToInventory(newItem);
            }
            inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());
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
        //inventoryModel.UpdateInventoryWeight(_data.weight * _data.quantity);
        _item.GetComponent<Item>().SetItemData(_data, _id);

    }

    private ItemData CreateItemData()
    {
        int itemIndex = Random.Range(0, inventoryModel.GetAllGameItemsCount());
        ItemData data = inventoryModel.GetItemAtIndex(itemIndex);
        data.quantity = 1;
        data.isShopItem = false;
        return data;
    }

    public void ShowItemsOfType(ItemType _itemType) => inventoryModel.ShowItemOfType(_itemType);
    public void ShowAllItems() => inventoryModel.ShowAllItems();

    private void OnItemBought(ItemData _data)
    {
        _data.isShopItem = false;
        if (!inventoryModel.CanAddItem())
        {
            GameService.instance.UIManager.ShowInventoryFullNotification();
            return;
        }
        (bool isItemPresent, int itemId) = IsItemPresentInInventory(_data.itemName);
        inventoryModel.AddInventoryWeight(_data.quantity * _data.weight);
        if (isItemPresent)
        {

            inventoryModel.AddItemCountWithId(itemId, _data.quantity);
        }
        else
        {
            GameObject newItem = CreateItemCards();
            int id = inventoryModel.CreateItemId();
            UpdateItemData(newItem, _data, id);
            inventoryModel.AddItemToInventory(newItem);
        }
        GameService.instance.UIManager.DecrementCoins(_data.buyingPrice * _data.quantity);
        inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());
    }

    private void OnItemSold(ItemData _data)
    {
        _data.isShopItem = true;

        (int itemId, int count) = GetItemIdAndQuantityInInventory(_data.itemName);
        if (itemId >= 0)
        {
            count -= _data.quantity;
            inventoryModel.DecreaseInventoryWeight(_data.quantity * _data.weight);
            inventoryModel.DecreaseItemCountWithId(itemId, _data.quantity);
            if (count <= 0)
            {
                GameObject itemToBeDeleted = inventoryModel.GetItemWithId(itemId);
                if (itemToBeDeleted)
                {
                    inventoryModel.RemoveItemFromInventory(itemToBeDeleted);
                    GameObject.Destroy(itemToBeDeleted);
                }
            }
        }
        GameService.instance.UIManager.IncrementCoins(_data.sellingPrice * _data.quantity);
        inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());
    }

    public void UpdateItemData(GameObject _item, ItemData _data, int _id)
    {
        _item.GetComponent<Item>().SetItemData(_data, _id);

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

    private (int, int) GetItemIdAndQuantityInInventory(string _name)
    {
        List<GameObject> inventoryItems = inventoryModel.GetInventoryItemsList();
        foreach (GameObject item in inventoryItems)
        {
            if (item.GetComponent<Item>().currentItemData.itemName == _name)
            {
                return (item.GetComponent<Item>().currentItemData.id, item.GetComponent<Item>().currentItemData.quantity);
            }
        }
        return (-1, -1);
    }
}

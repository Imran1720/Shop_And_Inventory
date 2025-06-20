using System.Collections.Generic;
using UnityEngine;

public class InventoryController
{
    private InventoryView inventoryView;
    private InventoryModel inventoryModel;

    public InventoryController(InventoryView _inventoryView, InventoryModel _inventoryModel)
    {
        inventoryModel = _inventoryModel;
        inventoryView = _inventoryView;

        inventoryView.InitializeShopController(this);
        inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());

        AddObservers();
    }

    private void AddObservers()
    {
        EventService.Instance.OnItemBought.AddListener(OnItemBought);
        EventService.Instance.OnItemSold.AddListener(OnItemSold);
    }

    public void RemoveObservers()
    {
        EventService.Instance.OnItemBought.RemoveListener(OnItemBought);
        EventService.Instance.OnItemSold.RemoveListener(OnItemSold);
    }

    public void GatherItems()
    {
        SoundManager.Instance.PlaySoundFX(Sounds.ITEM_GATHER);
        int numberOfCardsToSpawn = GetRandomSpawnCount();

        for (int i = 0; i < numberOfCardsToSpawn; i++)
        {
            if (!inventoryModel.CanAddItem())
            {
                GameService.Instance.UIManager.ShowInventoryFullNotification();
                return;
            }

            ItemData newDataItem = GetRandomItem();

            (bool isItemPresent, int itemId) = IsItemPresentInInventory(newDataItem.itemName);
            inventoryModel.AddInventoryWeight(newDataItem.quantity * newDataItem.weight);

            if (isItemPresent)
            {
                IncreaseItemCountWithId(itemId, newDataItem.quantity);
            }
            else
            {
                CreateNewItemCard(newDataItem);
            }
            inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());
        }
        EventService.Instance.OnItemGathered.InvokeEvent(inventoryModel.GetFirstItemInInventory());
    }

    private void CreateNewItemCard(ItemData newDataItem)
    {
        Item newItem = GameObject.Instantiate(inventoryModel.GetInventoryItemCard());
        newItem.transform.SetParent(inventoryView.GetItemContainer().transform, false);
        int id = inventoryModel.CreateItemId();
        UpdateItemData(newItem, newDataItem, id);
        inventoryModel.AddItemToInventory(newItem);
    }

    private ItemData GetRandomItem()
    {
        int itemIndex = Random.Range(0, inventoryModel.GetAllGameItemsCount());
        ItemData data = inventoryModel.GetItemAtIndex(itemIndex);
        data.quantity = 1;
        data.isShopItem = false;
        return data;
    }

    private void OnItemBought(ItemData _data)
    {
        _data.isShopItem = false;
        if (!inventoryModel.CanAddItem())
        {
            GameService.Instance.UIManager.ShowInventoryFullNotification();
            return;
        }

        (bool isItemPresent, int itemId) = IsItemPresentInInventory(_data.itemName);
        inventoryModel.AddInventoryWeight(_data.quantity * _data.weight);
        if (isItemPresent)
        {
            IncreaseItemCountWithId(itemId, _data.quantity);
        }
        else
        {
            CreateNewItemCard(_data);
        }
        GameService.Instance.UIManager.DecrementCoins(_data.buyingPrice * _data.quantity);
        inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());
    }

    private void OnItemSold(ItemData _data)
    {
        _data.isShopItem = true;

        (int itemId, int count) = GetItemIdAndQuantityFromInventory(_data.itemName);
        if (itemId >= 0)
        {
            count -= _data.quantity;
            inventoryModel.DecreaseInventoryWeight(_data.quantity * _data.weight);
            DecreaseItemCountWithId(itemId, _data.quantity);
            if (count <= 0)
            {
                Item itemToBeDeleted = GetItemWithId(itemId);
                if (itemToBeDeleted)
                {
                    inventoryModel.RemoveItemFromInventory(itemToBeDeleted);
                    GameObject.Destroy(itemToBeDeleted.gameObject);
                }
            }
        }
        GameService.Instance.UIManager.IncrementCoins(_data.sellingPrice * _data.quantity);
        inventoryView.SetInventoryWeight(inventoryModel.GetCurrentInventoryWeight());
    }

    private (bool, int) IsItemPresentInInventory(string _name)
    {
        List<Item> inventoryItems = inventoryModel.GetInventoryItemsList();
        foreach (Item item in inventoryItems)
        {
            if (item.currentItemData.itemName == _name)
            {
                return (true, item.currentItemData.id);
            }
        }
        return (false, -1);
    }

    private (int, int) GetItemIdAndQuantityFromInventory(string _name)
    {
        List<Item> inventoryItems = inventoryModel.GetInventoryItemsList();
        foreach (Item item in inventoryItems)
        {
            if (item.currentItemData.itemName == _name)
            {
                return (item.currentItemData.id, item.currentItemData.quantity);
            }
        }
        return (-1, -1);
    }

    public Item GetItemWithId(int _id)
    {
        List<Item> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        foreach (Item item in InventoryItemsList)
        {
            if (item.GetComponent<Item>().currentItemData.id == _id)
            {
                return item;
            }
        }
        return null;
    }

    public void DecreaseItemCountWithId(int _id, int _count)
    {
        List<Item> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        foreach (Item item in InventoryItemsList)
        {
            if (item.currentItemData.id == _id)
            {
                int itemcount = (item.currentItemData.quantity - _count);
                item.updateItemCount(itemcount);
                return;
            }
        }
    }

    public void IncreaseItemCountWithId(int _id, int _count)
    {
        List<Item> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        foreach (Item item in InventoryItemsList)
        {
            if (item.currentItemData.id == _id)
            {
                int itemcount = item.currentItemData.quantity + _count;
                item.updateItemCount(itemcount);
                return;
            }
        }
    }

    public void ShowAllItems()
    {
        SoundManager.Instance.PlaySoundFX(Sounds.ITEM_GATHER);
        List<Item> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        if (InventoryItemsList == null) return;

        foreach (Item _item in InventoryItemsList)
        {
            _item.gameObject.SetActive(true);
        }
    }

    public void ShowItemOfType(ItemType _type)
    {
        SoundManager.Instance.PlaySoundFX(Sounds.ITEM_GATHER);
        List<Item> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        if (InventoryItemsList == null) return;

        foreach (Item _item in InventoryItemsList)
        {
            _item.gameObject.SetActive(IsSameItemType(_type, _item));
        }
    }

    private static bool IsSameItemType(ItemType _type, Item _item) => _item.GetItemType() == _type;
    public void UpdateItemData(Item _item, ItemData _data, int _id) => _item.SetItemData(_data, _id);
    public int GetRandomSpawnCount() => Random.Range(1, inventoryModel.GetMaximumItemSpawnCount() + 1);
}

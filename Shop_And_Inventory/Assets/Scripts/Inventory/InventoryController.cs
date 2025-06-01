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
                AddItemCountWithId(itemId, newDataItem.quantity);
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
        GameObject newItemCard = GameObject.Instantiate(inventoryModel.GetInventoryItemCard());
        newItemCard.transform.SetParent(inventoryView.GetItemContainer().transform, false);
        return newItemCard;
    }

    public void SetItemData(GameObject _item, int _id, ItemData _data)
    {
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

    public void FilterItemsOfType(ItemType _itemType) => ShowItemOfType(_itemType);
    public void FilterAllItems() => ShowAllItems();

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
            AddItemCountWithId(itemId, _data.quantity);
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
            DecreaseItemCountWithId(itemId, _data.quantity);
            if (count <= 0)
            {
                GameObject itemToBeDeleted = GetItemWithId(itemId);
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

    public GameObject GetItemWithId(int _id)
    {
        List<GameObject> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        foreach (GameObject item in InventoryItemsList)
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
        List<GameObject> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        foreach (GameObject item in InventoryItemsList)
        {
            if (item.GetComponent<Item>().currentItemData.id == _id)
            {
                int itemcount = (item.GetComponent<Item>().currentItemData.quantity - _count);
                item.GetComponent<Item>().updateItemCount(itemcount);
                return;
            }
        }
    }

    public void AddItemCountWithId(int _id, int _count)
    {
        List<GameObject> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        foreach (GameObject item in InventoryItemsList)
        {
            if (item.GetComponent<Item>().currentItemData.id == _id)
            {
                int itemcount = item.GetComponent<Item>().currentItemData.quantity + _count;
                item.GetComponent<Item>().updateItemCount(itemcount);
                return;
            }
        }
    }

    public void ShowAllItems()
    {
        List<GameObject> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        if (InventoryItemsList == null) return;
        foreach (GameObject _item in InventoryItemsList)
        {
            _item.SetActive(true);
        }
    }

    public void ShowItemOfType(ItemType _type)
    {
        List<GameObject> InventoryItemsList = inventoryModel.GetInventoryItemsList();
        if (InventoryItemsList == null) return;
        foreach (GameObject _item in InventoryItemsList)
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
}

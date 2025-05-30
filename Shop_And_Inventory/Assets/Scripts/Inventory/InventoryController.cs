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
    }
    ~InventoryController()
    {
    }

    public void GatherItems()
    {
        int numberOfCardsToSpawn = inventoryModel.GetRandomSpawnCount();
        for (int i = 0; i < numberOfCardsToSpawn; i++)
        {
            GameObject newItem = CreateItemCards();
            int id = i + inventoryModel.CreateItemId();
            SetItemData(newItem, id);
            //inventoryModel.AddItemToInventory(newItem);
        }
    }

    private GameObject CreateItemCards()
    {
        GameObject newItemCard = GameObject.Instantiate(inventoryModel.GetShopItemCard());
        newItemCard.transform.SetParent(inventoryModel.GetItemContainer().transform, false);
        return newItemCard;
    }


    public void SetItemData(GameObject _item, int _id)
    {
        int itemIndex = Random.Range(0, inventoryModel.GetAllGameItemsCount());
        ItemData data = inventoryModel.GetItemAtIndex(itemIndex);
        _item.GetComponent<Item>().SetItemData(data, _id, false);

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

}

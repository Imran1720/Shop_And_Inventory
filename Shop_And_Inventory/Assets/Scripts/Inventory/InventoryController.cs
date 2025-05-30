using System.Collections.Generic;
using UnityEngine;

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

    //public void CreateShopItemsCards()
    //{
    //    int numberOfCardsToSpawn = shopModel.GetDefaultSpawnCount();
    //    for (int i = 0; i < numberOfCardsToSpawn; i++)
    //    {
    //        CreateItemCards();
    //    }

    //}

    //private void CreateItemCards()
    //{
    //    GameObject newItemCard = GameObject.Instantiate(shopModel.GetShopItemCard());
    //    newItemCard.transform.SetParent(shopModel.GetItemContainer().transform, false);
    //    shopModel.AddSpawnedItemCardToList(newItemCard);
    //}


    //public void UpdateItemCardsList()
    //{
    //    int numberOfCardsToSpawn = shopModel.GetDefaultSpawnCount();
    //    for (int i = 0; i < numberOfCardsToSpawn; i++)
    //    {
    //        Item itemCell = shopModel.GetItemCardAtIndex(i);
    //        if (itemCell != null)
    //        {
    //            int itemIndex = Random.Range(0, shopModel.GetAllGameItemsCount());
    //            itemCell.SetItemData(shopModel.GetGameItemAtIndex(itemIndex), i);
    //        }
    //    }
    //    EventService.Instance.OnShopUpdate.InvokeEvent(shopModel.GetFirstItemInShop());
    //}


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

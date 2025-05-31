using System.Collections.Generic;
using UnityEngine;

public class ShopController
{
    private ShopView shopView;
    private ShopModel shopModel;

    private float timer;

    public ShopController(ShopView _shopView, ShopModel _shopModel)
    {
        shopModel = _shopModel;
        shopView = _shopView;

        shopView.InitializeShopController(this);
        shopModel.InitializeShopController(this);

        shopModel.SetItemContainer(shopView.GetItemContainer());

        timer = shopModel.GetShopRefreshTime();
        EventService.Instance.OnItemBought.AddListener(OnBuyingItemFromShop);
        EventService.Instance.OnItemSold.AddListener(OnItemSold);
    }
    ~ShopController()
    {
        EventService.Instance.OnItemBought.RemoveListener(OnBuyingItemFromShop);
        EventService.Instance.OnItemSold.RemoveListener(OnItemSold);
    }

    public void CreateShopItemsCards()
    {
        int numberOfCardsToSpawn = shopModel.GetDefaultSpawnCount();
        for (int i = 0; i < numberOfCardsToSpawn; i++)
        {
            CreateItemCards();
        }
    }

    private void CreateItemCards()
    {
        GameObject newItemCard = GameObject.Instantiate(shopModel.GetShopItemCard());
        newItemCard.transform.SetParent(shopModel.GetItemContainer().transform, false);
        shopModel.AddSpawnedItemCardToList(newItemCard);
    }

    private GameObject CreateNewItemCard()
    {
        GameObject newItemCard = GameObject.Instantiate(shopModel.GetShopItemCard());
        newItemCard.transform.SetParent(shopModel.GetItemContainer().transform, false);
        shopModel.AddSpawnedItemCardToList(newItemCard);
        return newItemCard;
    }

    public void UpdateItemCardsList()
    {
        int numberOfCardsToSpawn = shopModel.GetDefaultSpawnCount();
        for (int i = 0; i < numberOfCardsToSpawn; i++)
        {
            Item itemCell = shopModel.GetItemCardAtIndex(i);
            if (itemCell != null)
            {
                int itemIndex = Random.Range(0, shopModel.GetAllGameItemsCount());
                ItemData newItem = shopModel.GetGameItemAtIndex(itemIndex);
                newItem.isShopItem = true;
                itemCell.SetItemData(newItem, shopModel.GetTotalItemsAdded());
                shopModel.IncrementTotalItemCount();
            }
        }
        //EventService.Instance.OnShopUpdate.InvokeEvent(shopModel.GetFirstItemInShop());
    }

    private void OnItemSold(ItemData _data)
    {
        GameObject itemObject = CreateNewItemCard();

        Item item = itemObject.GetComponent<Item>();
        item.SetItemData(_data, shopModel.GetTotalItemsAdded());
        shopModel.IncrementTotalItemCount();
    }

    public void ShowItemsOfType(ItemType _itemType) => shopModel.ShowItemOfType(_itemType);
    public void ShowAllItems() => shopModel.ShowAllItems();

    private void OnBuyingItemFromShop(ItemData _updatedData)
    {
        UpdateItemInList(_updatedData);
    }

    private void UpdateItemInList(ItemData data)
    {
        List<GameObject> shopItemsList = shopModel.GetShopItemsList();
        foreach (GameObject item in shopItemsList)
        {
            if (item != null && item.GetComponent<Item>().currentItemData.id == data.id)
            {
                int itemCount = item.GetComponent<Item>().currentItemData.quantity - data.quantity;
                if (itemCount <= 0)
                {
                    shopModel.RemoveItemFromShop(item);
                    GameObject.Destroy(item);
                }
                else
                {
                    item.GetComponent<Item>().updateItemCount(itemCount);
                }
                EventService.Instance.OnShopUpdate.InvokeEvent(shopModel.GetFirstItemInShop());
                return;
            }
        }
    }


    public void UpdateShopTimer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            ResetShop();
        }
    }

    public void ResetShop()
    {
        timer = shopModel.GetShopRefreshTime();
        ClearShop();
        CreateShopItemsCards();
        UpdateItemCardsList();
        EventService.Instance.OnShopRefresh.InvokeEvent(shopModel.GetFirstItemInShop());
    }

    private void ClearShop()
    {
        foreach (GameObject item in shopModel.GetShopItemsList())
        {
            GameObject.Destroy(item);
        }

        shopModel.RemoveAllItems();
    }


    public float GetTime() => timer;
}

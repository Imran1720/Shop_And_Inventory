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

        AddObservers();
        ResetShop();
    }

    private void AddObservers()
    {
        EventService.Instance.OnItemBought.AddListener(OnBuyingItemFromShop);
        EventService.Instance.OnItemSold.AddListener(OnItemSold);
    }

    public void RemoveObservers()
    {
        EventService.Instance.OnItemBought.RemoveListener(OnBuyingItemFromShop);
        EventService.Instance.OnItemSold.RemoveListener(OnItemSold);
    }

    public void CreateShopItemsCards()
    {
        int numberOfCardsToSpawn = shopModel.GetDefaultSpawnCount();
        for (int i = 0; i < numberOfCardsToSpawn; i++)
        {
            Item newItem = CreateNewItemCard();
            shopModel.AddSpawnedItemCardToList(newItem);
        }
    }

    private Item CreateNewItemCard()
    {
        Item newItemCard = GameObject.Instantiate(shopModel.GetShopItemCard());
        newItemCard.transform.SetParent(shopView.GetItemContainer().transform, false);
        return newItemCard;
    }

    public void UpdateItemInCardsList()
    {
        int numberOfCardsToSpawn = shopModel.GetDefaultSpawnCount();
        for (int i = 0; i < numberOfCardsToSpawn; i++)
        {
            Item itemCell = shopModel.GetItemCardAtIndex(i);
            if (itemCell != null)
            {
                ItemData newItem = GetRandomItem();
                newItem.isShopItem = true;
                itemCell.SetItemData(newItem, shopModel.GetTotalItemsAdded());
                shopModel.IncrementTotalItemCount();
            }
        }
    }

    private ItemData GetRandomItem()
    {
        int itemIndex = Random.Range(0, shopModel.GetTotalItemsInGame());
        return shopModel.GetGameItemAtIndex(itemIndex);
    }

    private void OnItemSold(ItemData _data)
    {
        SoundManager.Instance.PlaySoundFX(Sounds.ITEM_GATHER);

        Item newItem = CreateNewItemCard();
        shopModel.AddSpawnedItemCardToList(newItem);

        newItem.SetItemData(_data, shopModel.GetTotalItemsAdded());
        shopModel.IncrementTotalItemCount();
    }

    public void ShowItemsOfType(ItemType _itemType)
    {
        SoundManager.Instance.PlaySoundFX(Sounds.ITEM_GATHER);
        List<Item> shopItemsList = shopModel.GetShopItemsList();

        foreach (Item _item in shopItemsList)
        {
            if (_item.GetItemType() != _itemType)
            {
                _item.gameObject.SetActive(false);
            }
            else
            {
                _item.gameObject.SetActive(true);
            }
        }
    }

    public void ShowAllItems()
    {
        SoundManager.Instance.PlaySoundFX(Sounds.ITEM_GATHER);
        List<Item> shopItemsList = shopModel.GetShopItemsList();

        foreach (Item item in shopItemsList)
        {
            item.gameObject.SetActive(true);
        }
    }
    private void OnBuyingItemFromShop(ItemData _updatedData) => UpdateItemInList(_updatedData);

    private void UpdateItemInList(ItemData data)
    {
        List<Item> shopItemsList = shopModel.GetShopItemsList();
        foreach (Item item in shopItemsList)
        {
            if (item != null && item.currentItemData.id == data.id)
            {
                int itemCount = item.currentItemData.quantity - data.quantity;
                UpdateOrRemoveItem(item, itemCount);

                EventService.Instance.OnShopUpdate.InvokeEvent(shopModel.GetFirstItemInShop().currentItemData);
                return;
            }
        }
    }

    private void UpdateOrRemoveItem(Item item, int itemCount)
    {
        if (itemCount <= 0)
        {
            RemoveItemFromShop(item);
        }
        else
        {
            item.updateItemCount(itemCount);
        }
    }

    private void RemoveItemFromShop(Item item)
    {
        shopModel.RemoveItemFromList(item);
        shopView.DeleteItem(item);
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
        EmptyShop();
        CreateShopItemsCards();
        UpdateItemInCardsList();
        timer = shopModel.GetShopRefreshTime();
        SoundManager.Instance.PlaySoundFX(Sounds.CANCEL);
        EventService.Instance.OnShopRefresh.InvokeEvent(shopModel.GetFirstItemInShop().currentItemData);
    }

    private void EmptyShop()
    {
        shopView.EmptyShop(shopModel.GetShopItemsList());
        shopModel.EmptyShop();
    }

    public float GetTime() => (int)timer;
}

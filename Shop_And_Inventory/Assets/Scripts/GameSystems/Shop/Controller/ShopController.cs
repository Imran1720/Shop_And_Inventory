using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
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

        EventService.Instance.OnItemBought.AddListener(OnBuyingItemFromShop);
        EventService.Instance.OnItemSold.AddListener(OnItemSold);

        ResetShop();
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
            GameObject newItem = CreateNewItemCard();
            shopModel.AddSpawnedItemCardToList(newItem);
        }
    }

    private GameObject CreateNewItemCard()
    {
        GameObject newItemCard = GameObject.Instantiate(shopModel.GetShopItemCard());
        newItemCard.transform.SetParent(shopView.GetItemContainer().transform, false);
        return newItemCard;
    }

    public void UpdateItemInCardsList()
    {
        int numberOfCardsToSpawn = shopModel.GetDefaultSpawnCount();
        for (int i = 0; i < numberOfCardsToSpawn; i++)
        {
            Item itemCell = shopModel.GetItemCardAtIndex(i).GetComponent<Item>();
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

        GameObject itemObject = CreateNewItemCard();
        shopModel.AddSpawnedItemCardToList(itemObject);

        Item item = itemObject.GetComponent<Item>();
        item.SetItemData(_data, shopModel.GetTotalItemsAdded());
        shopModel.IncrementTotalItemCount();
    }

    public void ShowItemsOfType(ItemType _itemType)
    {
        List<GameObject> shopItemsList = shopModel.GetShopItemsList();

        foreach (GameObject _item in shopItemsList)
        {
            if (_item.GetComponent<Item>().GetItemType() != _itemType)
            {
                _item.SetActive(false);
            }
            else
            {
                _item.SetActive(true);
            }
        }
    }

    public void ShowAllItems()
    {
        List<GameObject> shopItemsList = shopModel.GetShopItemsList();

        foreach (GameObject item in shopItemsList)
        {
            item.SetActive(true);
        }
    }
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
                EventService.Instance.OnShopUpdate.InvokeEvent(shopModel.GetFirstItemInShop().GetComponent<Item>().currentItemData);
                return;
            }
        }
    }

    public void UpdateShopTimer()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SoundManager.Instance.PlaySoundFX(Sounds.CANCEL);
            ResetShop();
        }
    }

    public void ResetShop()
    {
        timer = shopModel.GetShopRefreshTime();
        ClearShop();
        CreateShopItemsCards();
        UpdateItemInCardsList();
        EventService.Instance.OnShopRefresh.InvokeEvent(shopModel.GetFirstItemInShop().GetComponent<Item>().currentItemData);
    }

    private void ClearShop()
    {
        foreach (GameObject item in shopModel.GetShopItemsList())
        {
            GameObject.Destroy(item);
        }
        shopModel.RemoveAllItems();
    }


    public float GetTime() => (int)timer;
}

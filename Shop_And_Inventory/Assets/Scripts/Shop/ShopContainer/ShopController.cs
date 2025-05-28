
using UnityEngine;

public class ShopController
{
    private ShopView shopView;
    private ShopModel shopModel;

    public ShopController(ShopView _shopView, ShopModel _shopModel)
    {
        shopModel = _shopModel;
        shopView = _shopView;

        shopView.InitializeShopController(this);
        shopModel.InitializeShopController(this);
    }

    private void CreateShopItemsCards()
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
        newItemCard.transform.SetParent(shopView.transform, false);
        shopModel.AddSpawnedItemCardToList(newItemCard);
    }


    //private void CreateItems()
    //{
    //    for (int i = 0; i < initialShopItemCount; i++)
    //    {
    //        CreateItemButton();
    //    }
    //    UpdateDisplayList();
    //}
}

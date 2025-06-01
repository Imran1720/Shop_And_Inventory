using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    [Header("Script")]
    private ShopView shopView;

    [Header("Objects")]
    [SerializeField] private GameObject shopPrefab;
    [SerializeField] private GameObject itemCardPrefab;
    [SerializeField] private ItemDataBase itemDatabase;

    [Header("Data")]
    [SerializeField] public int initialShopItemCount;
    [SerializeField] private int shopRefreshTime;

    private void Start()
    {
        GameObject shop = Instantiate(shopPrefab) as GameObject;
        shop.transform.SetParent(transform, false);

        shopView = shop.GetComponent<ShopView>();
        ShopModel shopModel = new ShopModel(itemCardPrefab, GameService.instance.GetGameItemList(), initialShopItemCount, shopRefreshTime);
        ShopController controller = new ShopController(shopView, shopModel);
    }
}

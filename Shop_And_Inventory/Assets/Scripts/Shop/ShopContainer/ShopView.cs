using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    private ShopController shopController;

    [Header("Container")]
    [SerializeField] private GameObject itemContainer;

    [Header("Filter Buttons")]
    [SerializeField] private Button weaponFilterButton;
    [SerializeField] private Button consumableFilterButton;
    [SerializeField] private Button treasureFilterButton;
    [SerializeField] private Button materialFilterButton;
    [SerializeField] private Button allFilterButton;

    [Header("Timer Text")]
    [SerializeField] private TextMeshProUGUI timerText;


    private void Start()
    {
        weaponFilterButton.onClick.AddListener(FilterWeapons);
        consumableFilterButton.onClick.AddListener(FilterConsumables);
        treasureFilterButton.onClick.AddListener(FilterTreasure);
        materialFilterButton.onClick.AddListener(FilterMaterials);
        allFilterButton.onClick.AddListener(FilterAll);


        shopController.ResetShop();
        //shopController.UpdateItemCardsList();
    }

    private void Update()
    {
        shopController.UpdateShopTimer();
        timerText.text = ((int)shopController.GetTime()).ToString();
    }
    private void FilterMaterials() => shopController.ShowItemsOfType(ItemType.MATERIAL);
    private void FilterConsumables() => shopController.ShowItemsOfType(ItemType.CONSUMABLE);
    private void FilterTreasure() => shopController.ShowItemsOfType(ItemType.TREASURE);
    private void FilterAll() => shopController.ShowAllItems();
    private void FilterWeapons() => shopController.ShowItemsOfType(ItemType.WEAPON);

    public void InitializeShopController(ShopController _controller) => shopController = _controller;

    public void PrintStatement(string data) => Debug.Log(data);

    public GameObject GetItemContainer() => itemContainer;
}

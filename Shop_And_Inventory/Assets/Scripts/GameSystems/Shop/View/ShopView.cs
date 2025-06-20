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

    private void Start() => SetButtonListeners();

    private void SetButtonListeners()
    {
        weaponFilterButton.onClick.AddListener(FilterWeapons);
        consumableFilterButton.onClick.AddListener(FilterConsumables);
        treasureFilterButton.onClick.AddListener(FilterTreasure);
        materialFilterButton.onClick.AddListener(FilterMaterials);
        allFilterButton.onClick.AddListener(FilterAll);
    }

    private void Update()
    {
        shopController.UpdateShopTimer();
        SetTimerText();
    }

    private void FilterAll() => shopController.ShowAllItems();
    private void FilterWeapons() => shopController.ShowItemsOfType(ItemType.WEAPON);
    private void FilterTreasure() => shopController.ShowItemsOfType(ItemType.TREASURE);
    private void FilterMaterials() => shopController.ShowItemsOfType(ItemType.MATERIAL);
    private void FilterConsumables() => shopController.ShowItemsOfType(ItemType.CONSUMABLE);

    private void OnDisable() => shopController.RemoveObservers();

    public void DeleteItem(Item item) => Destroy(item.gameObject);
    private void SetTimerText() => timerText.text = shopController.GetTime().ToString();
    public void InitializeShopController(ShopController _controller) => shopController = _controller;
    public void EmptyShop(List<Item> shopItemList) => shopItemList.ForEach(item => Destroy(item.gameObject));

    public GameObject GetItemContainer() => itemContainer;
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    private InventoryController inventoryController;

    [Header("Container")]
    [SerializeField] private GameObject itemContainer;

    [Header("Filter Buttons")]
    [SerializeField] private Button weaponFilterButton;
    [SerializeField] private Button consumableFilterButton;
    [SerializeField] private Button treasureFilterButton;
    [SerializeField] private Button materialFilterButton;
    [SerializeField] private Button allFilterButton;
    [SerializeField] private Button gatherItemButton;

    [Header("Weight Text")]
    [SerializeField] private TextMeshProUGUI weightText;

    void Start() => InitializeListeners();

    private void InitializeListeners()
    {
        weaponFilterButton.onClick.AddListener(FilterWeapons);
        consumableFilterButton.onClick.AddListener(FilterConsumables);
        treasureFilterButton.onClick.AddListener(FilterTreasure);
        materialFilterButton.onClick.AddListener(FilterMaterials);
        allFilterButton.onClick.AddListener(FilterAll);
        gatherItemButton.onClick.AddListener(GatherItems);
    }


    private void FilterWeapons() => inventoryController.ShowItemOfType(ItemType.WEAPON);
    private void FilterTreasure() => inventoryController.ShowItemOfType(ItemType.TREASURE);
    private void FilterMaterials() => inventoryController.ShowItemOfType(ItemType.MATERIAL);
    private void FilterConsumables() => inventoryController.ShowItemOfType(ItemType.CONSUMABLE);

    public GameObject GetItemContainer() => itemContainer;

    private void OnDestroy() => inventoryController.RemoveObservers();
    private void FilterAll() => inventoryController.ShowAllItems();
    private void GatherItems() => inventoryController.GatherItems();
    public void SetInventoryWeight(int value) => weightText.text = value.ToString();
    public void InitializeShopController(InventoryController _controller) => inventoryController = _controller;
}

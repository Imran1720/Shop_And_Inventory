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


    // Start is called before the first frame update
    void Start()
    {
        weaponFilterButton.onClick.AddListener(FilterWeapons);
        consumableFilterButton.onClick.AddListener(FilterConsumables);
        treasureFilterButton.onClick.AddListener(FilterTreasure);
        materialFilterButton.onClick.AddListener(FilterMaterials);
        allFilterButton.onClick.AddListener(FilterAll);
        gatherItemButton.onClick.AddListener(GatherItems);
    }

    private void FilterAll() => inventoryController.ShowAllItems();
    private void FilterWeapons() => inventoryController.FilterItemsOfType(ItemType.WEAPON);
    private void FilterTreasure() => inventoryController.FilterItemsOfType(ItemType.TREASURE);
    private void FilterMaterials() => inventoryController.FilterItemsOfType(ItemType.MATERIAL);
    private void FilterConsumables() => inventoryController.FilterItemsOfType(ItemType.CONSUMABLE);

    public void SetInventoryWeight(int value) => weightText.text = value.ToString();
    private void GatherItems() => inventoryController.GatherItems();
    public GameObject GetItemContainer() => itemContainer;
    public void InitializeShopController(InventoryController _controller) => inventoryController = _controller;
}

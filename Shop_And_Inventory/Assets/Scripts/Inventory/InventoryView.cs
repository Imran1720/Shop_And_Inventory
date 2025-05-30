using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : MonoBehaviour
{
    [Header("Container")]
    [SerializeField] private GameObject itemContainer;

    [Header("Filter Buttons")]
    [SerializeField] private Button weaponFilterButton;
    [SerializeField] private Button consumableFilterButton;
    [SerializeField] private Button treasureFilterButton;
    [SerializeField] private Button materialFilterButton;
    [SerializeField] private Button allFilterButton;

    [Header("Weight Text")]
    [SerializeField] private TextMeshProUGUI weightText;

    private InventoryController inventoryController;

    // Start is called before the first frame update
    void Start()
    {
        weaponFilterButton.onClick.AddListener(FilterWeapons);
        consumableFilterButton.onClick.AddListener(FilterConsumables);
        treasureFilterButton.onClick.AddListener(FilterTreasure);
        materialFilterButton.onClick.AddListener(FilterMaterials);
        allFilterButton.onClick.AddListener(FilterAll);
    }

    public void SetInventoryWeight(int value) => weightText.text = value.ToString();
    private void FilterMaterials() => inventoryController.ShowItemsOfType(ItemType.MATERIAL);
    private void FilterConsumables() => inventoryController.ShowItemsOfType(ItemType.CONSUMABLE);
    private void FilterTreasure() => inventoryController.ShowItemsOfType(ItemType.TREASURE);
    private void FilterAll() => inventoryController.ShowAllItems();
    private void FilterWeapons() => inventoryController.ShowItemsOfType(ItemType.WEAPON);

    public GameObject GetItemContainer() => itemContainer;
    public void InitializeShopController(InventoryController _controller) => inventoryController = _controller;

    public void PrintStatement(string data) => Debug.Log(data);
}

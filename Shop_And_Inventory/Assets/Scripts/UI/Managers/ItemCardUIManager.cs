using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCardUIManager : MonoBehaviour, IItemCardUIManager
{
    [Header("Item-Card Panels")]
    [SerializeField] private GameObject shopItemCardPanel;
    [SerializeField] private GameObject inventoryItemCardPanel;

    public bool IsShopCardActive() => shopItemCardPanel.activeSelf;

    public void OnItemSelected(bool isShopItem)
    {
        shopItemCardPanel.SetActive(isShopItem);
        inventoryItemCardPanel.SetActive(!isShopItem);
    }
}

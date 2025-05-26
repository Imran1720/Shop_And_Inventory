using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardHandler : MonoBehaviour
{
    public static ItemCardHandler Instance;
    private ItemSO currentItem;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemClassification;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemWeight;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [SerializeField] private Color legendary;
    [SerializeField] private Color epic;
    [SerializeField] private Color rare;
    [SerializeField] private Color common;

    private void Awake()
    {
        Instance = this;
    }
    public void SetItem(ItemSO _item)
    {
        currentItem = _item;

        SetData();
    }

    private void SetData()
    {
        itemName.color = GetTitleColor(currentItem.itemRarity);
        itemName.text = currentItem.name;
        icon.sprite = currentItem.icon;
        itemClassification.text = currentItem.itemClassification;
        itemQuantity.text = currentItem.quantity.ToString();
        itemPrice.text = currentItem.buyingPrice.ToString();
        itemWeight.text = currentItem.weight.ToString();
        itemDescription.text = currentItem.description;
    }

    private Color GetTitleColor(Rarity itemRarity)
    {
        switch (itemRarity)
        {
            case Rarity.LEGENDARY: return legendary;
            case Rarity.EPIC: return epic;
            case Rarity.RARE: return rare;
            case Rarity.COMMON: return common;
            default: return Color.white;

        }
    }
}

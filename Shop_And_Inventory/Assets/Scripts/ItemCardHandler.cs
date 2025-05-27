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

    [SerializeField] private Image itemCardBG;

    [SerializeField] private Color legendary;
    [SerializeField] private Color epic;
    [SerializeField] private Color rare;
    [SerializeField] private Color common;

    [SerializeField] private ItemSpriteSO bgSprites;

    [SerializeField] private Button buyButton;

    private int itemcount;
    private void Awake()
    {
        Instance = this;
        buyButton.onClick.AddListener(DecrementCount);
    }
    public void SetItem(ItemSO _item)
    {
        currentItem = _item;
        itemcount = currentItem.quantity;
        UpdateData();
    }

    private void UpdateData()
    {
        itemCardBG.sprite = GetBGSprite(currentItem.itemRarity);
        itemName.color = GetTitleColor(currentItem.itemRarity);
        itemName.text = currentItem.name;
        icon.sprite = currentItem.icon;
        itemClassification.text = currentItem.itemClassification;
        itemQuantity.text = itemcount.ToString();
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

    private Sprite GetBGSprite(Rarity itemRarity)
    {
        switch (itemRarity)
        {
            case Rarity.LEGENDARY: return bgSprites.legendaryBG;
            case Rarity.EPIC: return bgSprites.epicBG;
            case Rarity.RARE: return bgSprites.rareBG;
            case Rarity.COMMON: return bgSprites.commonBG;
            default: return bgSprites.veryCommonBG;

        }
    }

    private void DecrementCount()
    {
        if (itemcount >= 1)
            itemcount--;

        UpdateData();

        if (itemcount <= 0)
        {
            //Deleted item from shop
        }
    }
}

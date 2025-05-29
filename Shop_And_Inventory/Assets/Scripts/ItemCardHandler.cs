using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardHandler : MonoBehaviour
{
    public static ItemCardHandler Instance;
    private ItemData currentItem;

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
    [SerializeField] private Button decrementButton;
    [SerializeField] private Button incrementButton;
    [SerializeField] private TextMeshProUGUI itemCountTobeBoughtText;
    [SerializeField] private GameObject popUPPannel;

    private int itemCount;
    private int itemToBeBoughtCount;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
        decrementButton.onClick.AddListener(DecrementCount);
        incrementButton.onClick.AddListener(IncrementCount);
    }
    public void SetItem(ItemData _item)
    {
        currentItem = _item;
        itemCount = currentItem.quantity;
        itemToBeBoughtCount = 1;
        itemCountTobeBoughtText.text = itemToBeBoughtCount.ToString();
        UpdateData();
    }

    private void UpdateData()
    {
        itemCardBG.sprite = GetBGSprite(currentItem.itemRarity);
        itemName.color = GetTitleColor(currentItem.itemRarity);
        itemName.text = currentItem.itemName;
        icon.sprite = currentItem.icon;
        itemClassification.text = currentItem.itemClassification;
        itemQuantity.text = itemCount.ToString();
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
        if (itemToBeBoughtCount >= 1)
        {
            itemToBeBoughtCount--;
        }

        itemCountTobeBoughtText.text = itemToBeBoughtCount.ToString();
    }

    private void IncrementCount()
    {
        if (itemToBeBoughtCount < itemCount)
        {
            itemToBeBoughtCount++;
        }

        itemCountTobeBoughtText.text = itemToBeBoughtCount.ToString();
    }

    private void BuyItem()
    {
        if (itemToBeBoughtCount <= 0)
        {
            return;
        }
        int cost = itemToBeBoughtCount * currentItem.buyingPrice;
        popUPPannel.SetActive(true);
        PopUpManager.Instance.SetData(currentItem, itemToBeBoughtCount);
    }
}

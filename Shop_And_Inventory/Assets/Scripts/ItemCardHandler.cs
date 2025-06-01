using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardHandler : MonoBehaviour
{
    public static ItemCardHandler Instance;

    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemClassification;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemWeight;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemCountTobeBoughtText;

    [Header("IMAGES")]
    [SerializeField] private Image icon;
    [SerializeField] private Image itemCardBG;

    [Header("SCRIPTABLE OBJECTS")]
    [SerializeField] private ItemSpriteSO bgSprites;

    [Header("BUTTONS")]
    [SerializeField] private Button buyButton;
    [SerializeField] private Button decrementButton;
    [SerializeField] private Button incrementButton;

    [Header("PANELS")]
    [SerializeField] private GameObject popUPPannel;

    private ItemData currentItem;
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
            case Rarity.LEGENDARY: return Color.yellow;
            case Rarity.EPIC: return Color.magenta;
            case Rarity.RARE: return Color.cyan;
            case Rarity.COMMON: return Color.green;
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
        //PopUpManager.Instance.SetData(currentItem, itemToBeBoughtCount);
    }
}

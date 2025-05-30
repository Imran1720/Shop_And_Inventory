using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardView : MonoBehaviour
{
    [Header("TEXTS")]
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemClassification;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemWeight;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemTobeBoughtCountText;

    [Header("IMAGES")]
    [SerializeField] private Image icon;
    [SerializeField] private Image itemCardBG;

    [Header("BUTTONS")]
    [SerializeField] private Button buyButton;
    [SerializeField] private Button decrementButton;
    [SerializeField] private Button incrementButton;



    private ItemCardController itemCardController;


    private void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
        decrementButton.onClick.AddListener(DecreaseItemCount);
        incrementButton.onClick.AddListener(IncreaseItemCOunt);
    }

    public void RefreshUI(ItemData _data, Sprite _itemCardBG, Color _color, int count)
    {
        itemCardBG.sprite = _itemCardBG;
        itemName.color = _color;
        itemName.text = _data.itemName;
        icon.sprite = _data.icon;
        itemClassification.text = _data.itemClassification;
        itemQuantity.text = "Count : " + _data.quantity.ToString();
        itemPrice.text = _data.buyingPrice.ToString();
        itemWeight.text = _data.weight.ToString();
        itemDescription.text = _data.description;
        itemTobeBoughtCountText.text = count.ToString();
    }

    private void DecreaseItemCount() => itemCardController.DecreaseItemCount();

    private void IncreaseItemCOunt() => itemCardController.IncreaseItemCount();

    private void BuyItem()
    {
        UIUtility.Instance.EnableBuyPopPanel();
        itemCardController.BuyItem();
    }
    public void SetController(ItemCardController _controller) => itemCardController = _controller;

    public void UpdateBuyingItemCount(int value) => itemTobeBoughtCountText.text = value.ToString();

    public void Print(string text) { Debug.Log(text); }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardView : MonoBehaviour
{
    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemClassificationText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;
    [SerializeField] private TextMeshProUGUI itemPriceText;
    [SerializeField] private TextMeshProUGUI itemWeightText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;
    [SerializeField] private TextMeshProUGUI itemToBeBoughtCountText;

    [Header("Image Components")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private Image itemCardBackground;

    [Header("Button Components")]
    [SerializeField] private Button buyButton;
    [SerializeField] private Button decrementButton;
    [SerializeField] private Button incrementButton;

    private ItemCardController itemCardController;

    private void Start()
    {
        buyButton.onClick.AddListener(OnBuyButtonClicked);
        decrementButton.onClick.AddListener(OnDecrementButtonClicked);
        incrementButton.onClick.AddListener(OnIncrementButtonClicked);
    }

    public void RefreshUI(ItemData _data, Sprite _itemCardBackground, Color _nameColor, int _buyingCount)
    {

        int price = _data.isShopItem == true ? _data.buyingPrice : _data.sellingPrice;

        itemCardBackground.sprite = _itemCardBackground;
        itemNameText.color = _nameColor;
        itemNameText.text = _data.itemName;

        itemIcon.sprite = _data.icon;
        itemClassificationText.text = _data.itemClassification;
        itemQuantityText.text = "Count : " + _data.quantity.ToString();

        itemPriceText.text = price.ToString();
        itemWeightText.text = _data.weight.ToString();

        itemDescriptionText.text = _data.description;
        itemToBeBoughtCountText.text = _buyingCount.ToString();
    }

    private void OnDecrementButtonClicked() => itemCardController.DecreaseItemCount();

    private void OnIncrementButtonClicked() => itemCardController.IncreaseItemCount();

    private void OnBuyButtonClicked()
    {

        itemCardController.BuyItem();
    }
    public void SetController(ItemCardController _controller) => itemCardController = _controller;

    public void UpdateBuyingItemCount(int value) => itemToBeBoughtCountText.text = value.ToString();

}

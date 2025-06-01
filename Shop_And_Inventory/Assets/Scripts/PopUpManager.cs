using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance;

    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button cancelButton;

    ItemData itemData;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        acceptButton.onClick.AddListener(BuyItem);
        cancelButton.onClick.AddListener(ClosePopUp);
    }
    public void SetData(ItemData _itemData, int itemCount)
    {
        itemData = _itemData;
        itemData.quantity -= itemCount;
        promptText.text = $"Do you want to buy {itemData.itemName} x{itemCount} for {itemData.buyingPrice * itemCount}?";
    }

    private void ClosePopUp()
    {
        gameObject.SetActive(false);
    }

    private void BuyItem()
    {
        //event to update count in the shop
        gameObject.SetActive(false);

        EventService.Instance.OnItemBought.InvokeEvent(itemData);
    }

}

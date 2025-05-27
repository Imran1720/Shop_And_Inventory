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

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ClosePopUp();
        acceptButton.onClick.AddListener(BuyItem);
        cancelButton.onClick.AddListener(ClosePopUp);
    }
    public void SetData(string itemName, int itemCount, int itemCost)
    {
        promptText.text = $"Do you want to buy {itemName} x{itemCount} for {itemCost}?";

    }

    private void ClosePopUp()
    {
        gameObject.SetActive(false);
    }

    private void BuyItem()
    {
        //event to update count in the shop
        Debug.Log($"Bought item");
        gameObject.SetActive(false);
    }

}

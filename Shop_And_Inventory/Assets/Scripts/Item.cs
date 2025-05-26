using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private Image containerImage;
    ItemSO currentItemData;

    private void Start()
    {
    }

    public void UpdateItemData(ItemSO item)
    {
        currentItemData = item;
        itemIcon.sprite = item.icon;
        itemCount.text = item.quantity.ToString();
        containerImage.sprite = UIUtility.Instance.GetButtonRarity(item.itemRarity);
    }

    public ItemType GetItemType()
    {
        return currentItemData.itemType;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }
}

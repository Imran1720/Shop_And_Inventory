using System;
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
    public ItemData currentItemData;

    public void SetItemData(ItemData item, int id)
    {
        currentItemData = item;
        currentItemData.id = id;
        SetItemVisualData(currentItemData);
    }

    private void SetItemVisualData(ItemData item)
    {
        itemIcon.sprite = item.icon;
        itemCount.text = item.quantity.ToString();
        containerImage.sprite = GameService.Instance.GetButtonRarity(item.itemRarity);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySoundFX(Sounds.BUTTON_CLICK);
        EventService.Instance.OnItemSelected.InvokeEvent(currentItemData);
    }

    public void updateItemCount(int count)
    {
        currentItemData.quantity = count;
        itemCount.text = count.ToString();
    }

    public ItemType GetItemType() => currentItemData.itemType;
}



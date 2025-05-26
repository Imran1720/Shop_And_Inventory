using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private GameObject itemButton;
    [SerializeField] private List<ItemSO> itemsDataList;
    //[SerializeField] public List<Item> itemsToSetList;
    [SerializeField] public List<GameObject> itemsObjectList;
    [SerializeField] private ItemDataBase itemCollection;

    [SerializeField] private Button weaponFilterButton;
    [SerializeField] private Button consumableFilterButton;
    [SerializeField] private Button treasureFilterButton;
    [SerializeField] private Button materialFilterButton;
    [SerializeField] private Button allFilterButton;

    [SerializeField] private int initialShopItemCount;
    [SerializeField] private int shopRefreshTime;
    [SerializeField] private TextMeshProUGUI timerText;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        UpdateItemDataList();
        timer = shopRefreshTime;
        weaponFilterButton.onClick.AddListener(ShowOnlyWeapons);
        consumableFilterButton.onClick.AddListener(ShowOnlyConsumables);
        treasureFilterButton.onClick.AddListener(ShowOnlyTreasure);
        materialFilterButton.onClick.AddListener(ShowOnlyMaterial);
        allFilterButton.onClick.AddListener(ShowAllItems);

        CreateItems();

    }

    private void Update()
    {
        UpdateTime();
    }
    private void UpdateItemDataList()
    {
        foreach (ItemSO item in itemCollection.weaponsList)
        {
            itemsDataList.Add(item);
        }
        foreach (ItemSO item in itemCollection.treasureList)
        {
            itemsDataList.Add(item);
        }
        foreach (ItemSO item in itemCollection.ConsumableList)
        {
            itemsDataList.Add(item);
        }
        foreach (ItemSO item in itemCollection.materialList)
        {
            itemsDataList.Add(item);
        }
    }
    private void CreateItems()
    {
        int currentItemsInShop = 0;
        for (int i = 0; i < itemsDataList.Count; i++)
        {
            if (currentItemsInShop >= initialShopItemCount)
            {
                break;
            }
            GameObject currentItem = Instantiate(itemButton);
            currentItem.transform.SetParent(this.transform, false);
            itemsObjectList.Add(currentItem);
            currentItemsInShop++;
        }
        UpdateDisplayList();
    }

    public void UpdateDisplayList()
    {
        for (int i = 0; i < itemsObjectList.Count; i++)
        {
            Item item = itemsObjectList[i].transform.GetComponent<Item>();
            if (item != null)
            {
                item.UpdateItemData(itemsDataList[i]);
            }
        }

    }

    public void ShowOnlyWeapons()
    {
        foreach (GameObject item in itemsObjectList)
        {
            if (item.GetComponent<Item>().GetItemType() != ItemType.WEAPON)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }
    }

    public void ShowOnlyConsumables()
    {
        foreach (GameObject item in itemsObjectList)
        {
            if (item.GetComponent<Item>().GetItemType() != ItemType.CONSUMABLE)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }
    }
    public void ShowOnlyTreasure()
    {
        foreach (GameObject item in itemsObjectList)
        {
            if (item.GetComponent<Item>().GetItemType() != ItemType.TREASURE)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }
    }

    public void ShowOnlyMaterial()
    {
        foreach (GameObject item in itemsObjectList)
        {
            if (item.GetComponent<Item>().GetItemType() != ItemType.MATERIAL)
            {
                item.SetActive(false);
            }
            else
            {
                item.SetActive(true);
            }
        }
    }

    public void ShowAllItems()
    {
        foreach (GameObject item in itemsObjectList)
        {
            item.SetActive(true);
        }
    }


    public void UpdateTime()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = shopRefreshTime;
        }
        timerText.text = ((int)timer).ToString();
    }
}

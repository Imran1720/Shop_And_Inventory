
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandler : MonoBehaviour
{
    [SerializeField] private GameObject itemButton;
    [SerializeField] private List<ItemData> itemsDataList;
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


    private ItemData currentItemData;

    private void OnEnable()
    {
        EventService.Instance.OnItemBought.AddListener(UpdateListAfterBuying);
    }

    private void OnDisable()
    {

        EventService.Instance.OnItemBought.RemoveListener(UpdateListAfterBuying);
    }
    void Start()
    {
        itemsDataList = new List<ItemData>();
        timer = shopRefreshTime;
        UpdateItemDataList();
        AddButtonListeners();

        CreateItems();

    }

    private void AddButtonListeners()
    {
        weaponFilterButton.onClick.AddListener(ShowOnlyWeapons);
        consumableFilterButton.onClick.AddListener(ShowOnlyConsumables);
        treasureFilterButton.onClick.AddListener(ShowOnlyTreasure);
        materialFilterButton.onClick.AddListener(ShowOnlyMaterial);
        allFilterButton.onClick.AddListener(ShowAllItems);
    }

    private void Update()
    {
        // UpdateTime();
    }
    private void UpdateItemDataList()
    {
        foreach (ItemSO item in itemCollection.weaponsList)
        {
            CreateAndAppendDataToList(item);
        }
        foreach (ItemSO item in itemCollection.treasureList)
        {
            CreateAndAppendDataToList(item);
        }
        foreach (ItemSO item in itemCollection.ConsumableList)
        {
            CreateAndAppendDataToList(item);
        }
        foreach (ItemSO item in itemCollection.materialList)
        {
            CreateAndAppendDataToList(item);
        }
    }

    private void CreateAndAppendDataToList(ItemSO item)
    {
        currentItemData = CreateItem(item);
        itemsDataList.Add(currentItemData);

    }

    private void CreateItems()
    {
        for (int i = 0; i < initialShopItemCount; i++)
        {
            CreateItemButton();
        }
        UpdateDisplayList();
    }

    private void CreateItemButton()
    {
        GameObject currentItem = Instantiate(itemButton);
        currentItem.transform.SetParent(this.transform, false);
        itemsObjectList.Add(currentItem);
    }

    public void UpdateDisplayList()
    {
        for (int i = 0; i < initialShopItemCount; i++)
        {
            Item itemCell = GetItem(i);
            if (itemCell != null)
            {
                int itemIndex = Random.Range(0, itemsDataList.Count);
                itemCell.SetItemData(itemsDataList[itemIndex], i);
            }
        }
        ItemCardHandler.Instance.SetItem(itemsObjectList[0].GetComponent<Item>().currentItemData);
    }

    private Item GetItem(int itemIndex)
    {
        return itemsObjectList[itemIndex].transform.GetComponent<Item>();
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
            UpdateDisplayList();
            ItemCardHandler.Instance.SetItem(itemsObjectList[0].GetComponent<Item>().currentItemData);
        }
        timerText.text = ((int)timer).ToString();
    }


    private ItemData CreateItem(ItemSO item)
    {
        ItemData newData = new ItemData();
        newData.id = itemsDataList.Count;
        newData.itemName = item.itemName;
        newData.itemType = item.itemType;
        newData.itemRarity = item.itemRarity;
        newData.icon = item.icon;
        newData.buyingPrice = item.buyingPrice;
        newData.sellingPrice = item.buyingPrice;
        newData.weight = item.weight;
        newData.quantity = item.quantity;
        newData.description = item.description;

        return newData;
    }

    private void UpdateListAfterBuying(ItemData _updatedData)
    {

        if (_updatedData.quantity <= 0)
        {
            Destroy(itemsObjectList[_updatedData.id]);
        }
        else
        {
            itemsObjectList[_updatedData.id].transform.GetComponent<Item>().updateItemCount(_updatedData.quantity);

            ItemCardHandler.Instance.SetItem(itemsObjectList[0].GetComponent<Item>().currentItemData);
        }

    }
}


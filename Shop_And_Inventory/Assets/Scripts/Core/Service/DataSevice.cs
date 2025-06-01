using System.Collections.Generic;
public class DataSevice
{
    private ItemDataBase itemDataBase;
    private List<ItemData> allGameItems;

    public DataSevice(ItemDataBase _itemDataBase)
    {
        itemDataBase = _itemDataBase;
        allGameItems = new List<ItemData>();
        LoadAllItemData();
    }

    public List<ItemData> GetAllGameItemsList() => allGameItems;

    private void LoadAllItemData()
    {
        LoadItemFrom(itemDataBase.materialList);
        LoadItemFrom(itemDataBase.ConsumableList);
        LoadItemFrom(itemDataBase.treasureList);
        LoadItemFrom(itemDataBase.weaponsList);
    }


    private void LoadItemFrom(ItemSO[] _itemsList)
    {
        foreach (ItemSO item in _itemsList)
        {
            allGameItems.Add(CreateItemDataFromSO(item));
        }
    }

    private ItemData CreateItemDataFromSO(ItemSO item)
    {
        ItemData newData = new ItemData();
        newData.id = allGameItems.Count;
        newData.itemClassification = item.itemClassification;
        newData.itemName = item.itemName;
        newData.itemType = item.itemType;
        newData.itemRarity = item.itemRarity;
        newData.icon = item.icon;
        newData.buyingPrice = item.buyingPrice;
        newData.sellingPrice = item.sellingPrice;
        newData.weight = item.weight;
        newData.quantity = item.quantity;
        newData.description = item.description;

        return newData;
    }
}

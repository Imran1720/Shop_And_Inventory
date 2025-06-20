using UnityEngine;
public struct ItemData
{
    public bool isShopItem;

    public int id;
    public int weight;
    public int quantity;
    public int buyingPrice;
    public int sellingPrice;
    public string description;

    public ItemType itemType;
    public Rarity itemRarity;
    public Sprite icon;

    public string itemName;
    public string itemClassification;
}
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Item", fileName = "Item")]
public class ItemSO : ScriptableObject
{
    [Header("Description")]
    public string itemName;
    public string itemClassification;
    [TextArea(3, 10)] public string description;

    [Header("Types")]
    public ItemType itemType;
    public Rarity itemRarity;

    [Header("Icon")]
    public Sprite icon;

    [Header("Numberical Data")]
    public int id;
    public int weight;
    public int quantity;
    public int buyingPrice;
    public int sellingPrice;
}

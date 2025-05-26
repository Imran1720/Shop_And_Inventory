using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "ScriptableObject/Item", fileName = "Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public string itemClassification;
    public ItemType itemType;
    public Rarity itemRarity;
    public Sprite icon;
    public int buyingPrice;
    public int sellingPrice;
    public int weight;
    public int quantity;
    [TextArea(3, 10)]
    public string description;
}

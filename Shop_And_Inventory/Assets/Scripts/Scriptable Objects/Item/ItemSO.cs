using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Rarity itemRarity;
    public Sprite icon;
    public int buyingPrixe;
    public int sellingPrice;
    public int weight;
    public int quantity;
    [TextArea(3, 10)]
    public string description;
}

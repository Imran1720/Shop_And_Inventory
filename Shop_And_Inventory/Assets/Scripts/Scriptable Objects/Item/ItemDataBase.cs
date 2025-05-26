using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Database", fileName = "DataBase")]
public class ItemDataBase : ScriptableObject
{
    public ItemSO[] weaponsList;
    public ItemSO[] materialList;
    public ItemSO[] ConsumableList;
    public ItemSO[] treasureList;
}

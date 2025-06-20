using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{
    [Header("Script")]
    private InventoryView inventoryView;

    [Header("Objects")]
    [SerializeField] private InventoryView inventoryPrefab;
    [SerializeField] private Item itemCardPrefab;
    [SerializeField] private ItemDataBase itemDatabase;

    [Header("Objects")]
    [SerializeField] private int maxInvnetoryWeight;

    private void Start()
    {
        CreateInventory();

        InventoryModel inventoryModel = new InventoryModel(itemCardPrefab, GameService.Instance.GetGameItemList(), maxInvnetoryWeight);
        InventoryController controller = new InventoryController(inventoryView, inventoryModel);
    }

    private void CreateInventory()
    {
        inventoryView = Instantiate(inventoryPrefab);
        inventoryView.transform.SetParent(transform, false);
    }
}

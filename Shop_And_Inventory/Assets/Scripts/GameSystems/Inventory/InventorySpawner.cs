using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{
    [Header("Script")]
    private InventoryView inventoryView;

    [Header("Objects")]
    [SerializeField] private GameObject inventoryPrefab;
    [SerializeField] private GameObject itemCardPrefab;
    [SerializeField] private ItemDataBase itemDatabase;

    [Header("Objects")]
    [SerializeField] private int maxInvnetoryWeight;

    private void Start()
    {
        GameObject inventory = Instantiate(inventoryPrefab) as GameObject;
        inventory.transform.SetParent(transform, false);

        inventoryView = inventory.GetComponent<InventoryView>();
        InventoryModel inventoryModel = new InventoryModel(itemCardPrefab, GameService.instance.GetGameItemList(), maxInvnetoryWeight);
        InventoryController controller = new InventoryController(inventoryView, inventoryModel);
    }
}

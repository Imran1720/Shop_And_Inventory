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

    [Header("Objects")]
    [SerializeField] private int maxInvnetoryWeight;
    private void Start()
    {

        GameObject inventory = Instantiate(inventoryPrefab) as GameObject;
        inventory.transform.SetParent(transform, false);

        inventoryView = inventory.GetComponent<InventoryView>();
        InventoryModel inventoryModel = new InventoryModel(itemCardPrefab, maxInvnetoryWeight);

        InventoryController controller = new InventoryController(inventoryView, inventoryModel);
    }
}

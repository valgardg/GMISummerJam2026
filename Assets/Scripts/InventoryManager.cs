using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private float inventoryCapacity = 4f; // Example capacity
    // private float currentInventoryWeight = 0f;

    private List<GameObject> inventoryItems = new List<GameObject>();

    public static InventoryManager Instance { get; private set; }

    public static event Action<GameObject> OnAddItemToInventory;

    public float dropDistance = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SlottedItem.OnDropItemFromInventory += HandleItemDrop;
    }

    public void TryAddItem(GameObject itemGO)
    {
        // if (currentInventoryWeight + itemData.itemWeight > inventoryCapacity)
        if (inventoryItems.Count >= inventoryCapacity) // Using item count for capacity check
        {
            Debug.Log("Cannot add item. Inventory is full!");
            return;
        }

        AddItem(itemGO);        
    }

    private void AddItem(GameObject itemGO)
    {
        ItemData itemData = itemGO.GetComponent<ItemPickup>().itemData;
        // currentInventoryWeight += itemData.itemWeight;
        inventoryItems.Add(itemGO);
        itemGO.SetActive(false); // Hide the item in the world
        Debug.Log($"Added {itemData.itemName} with weight {itemData.itemWeight} to inventory.");
        OnAddItemToInventory?.Invoke(itemGO);
    }

    private void HandleItemDrop(GameObject itemGO)
    {
        if (inventoryItems.Contains(itemGO))
        {
            itemGO.transform.position = PlayerController.Instance.transform.position + Vector3.right * PlayerController.Instance.FacingDirection * dropDistance;
            itemGO.SetActive(true); // Show the item in the world again
            inventoryItems.Remove(itemGO);
            Debug.Log($"Dropped {itemGO.name} from inventory.");
            
        }
    }
}
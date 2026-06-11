using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject emptySlotPrefab;
    public GameObject itemSlotPrefab;
    public Transform inventoryPanel;

    private List<GameObject> itemSlots = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 4; i++) // Assuming a fixed inventory size of 4
        {
            GameObject slot = Instantiate(emptySlotPrefab, inventoryPanel);
            itemSlots.Add(slot);
        }

        // Subscribe to inventory changes (you would need to implement this event in InventoryManager)
        InventoryManager.OnAddItemToInventory += UpdateInventoryUI;
        SlottedItem.OnDropItemFromInventory += HandleItemDrop;
    }

    void UpdateInventoryUI(GameObject itemGO)
    {
        ItemData itemData = itemGO.GetComponent<ItemPickup>().itemData;

        // Remove the last slot (rightmost empty slot)
        GameObject lastSlot = itemSlots[itemSlots.Count - 1];
        itemSlots.RemoveAt(itemSlots.Count - 1);
        Destroy(lastSlot);

        // Instantiate a new item slot and insert it at the front
        GameObject newSlot = Instantiate(itemSlotPrefab, inventoryPanel);
        newSlot.transform.SetAsFirstSibling();
        itemSlots.Insert(0, newSlot);

        // Pass the item data to the new slot so it can display the right icon/name/etc.
        newSlot.GetComponent<SlottedItem>().Instantiate(itemGO);
    }

    private void HandleItemDrop(GameObject itemGO)
    {
        // When an item is dropped, we need to remove it from the inventory UI and add an empty slot at the end
        GameObject slot = Instantiate(emptySlotPrefab, inventoryPanel);
        itemSlots.Add(slot);
    }
}

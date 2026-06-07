using System;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance { get; private set; }

    private bool isPlayerInBase = false;
    public bool IsPlayerInBase => isPlayerInBase;

    public float sustenanceSupply = 0f;
    public float warmthSupply = 0f;
    public float entertainmentSupply = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInBase = true;
        } 
        else if (other.CompareTag("Item"))
        {
            // Handle item collection logic here
            Debug.Log("Item collected!");

            ItemPickup itemPickup = other.gameObject.transform.parent.GetComponent<ItemPickup>();
            Debug.Log($"ItemPickup component found: {itemPickup != null}");
            ItemData itemData = itemPickup.itemData;
            Debug.Log($"Collected item: {itemData.itemName} of type {itemData.itemType} with value {itemData.itemValue}");
            switch (itemData.itemType)
            {
                case ItemType.Consumable:
                    sustenanceSupply += itemData.itemValue;
                    break;
                case ItemType.Warmth:
                    warmthSupply += itemData.itemValue;
                    break;
                case ItemType.Entertainment:
                    entertainmentSupply += itemData.itemValue;
                    break;
            }
            Destroy(other.gameObject.transform.parent.gameObject); // Destroy the parent GameObject of the item pickup
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInBase = false;
        }
    }
}

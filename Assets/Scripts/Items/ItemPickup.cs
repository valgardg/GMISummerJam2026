using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [Header("Item Properties")]
    public ItemData itemData;

    public GameObject pickupPrompt;

    private bool isPlayerInRange = false;

    public static event Action<GameObject> OnInspectItem;

    void Start()
    {
        pickupPrompt.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInRange)
        {
            Debug.Log($"Inspecting {itemData.itemName}!");
            // InventoryManager.Instance.TryAddItem(gameObject);
            OnInspectItem?.Invoke(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        TogglePickupPrompt();
        isPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        TogglePickupPrompt();
        isPlayerInRange = false;
    }

    private void TogglePickupPrompt()
    {
        pickupPrompt.SetActive(!pickupPrompt.activeSelf);
    }
}

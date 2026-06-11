using System;
using UnityEngine;
using UnityEngine.UI;

public class SlottedItem : MonoBehaviour
{
    private GameObject itemGO;

    public static event Action<GameObject> OnDropItemFromInventory;

    public void Instantiate(GameObject itemGO)
    {
        this.itemGO = itemGO;
        ItemData itemData = itemGO.GetComponent<ItemPickup>().itemData;
        GetComponentInChildren<Button>().GetComponent<Image>().sprite = itemData.itemSprite;
    }

    public void DropItem()
    {
        OnDropItemFromInventory?.Invoke(itemGO);
        Destroy(gameObject); // Remove the item from the inventory UI
    }
}

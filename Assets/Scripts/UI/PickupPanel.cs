using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickupPrompt : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public TMP_Text itemWeightText;
    public TMP_Text itemStatsText;
    public Image itemSprite;

    private GameObject itemGO;

    public void Start()
    {
        ItemPickup.OnInspectItem += DisplayPickupPanel;
        gameObject.SetActive(false);
    }

    public void DisplayPickupPanel(GameObject itemGO)
    {
        gameObject.SetActive(true);
        this.itemGO = itemGO;
        ItemData itemData = itemGO.GetComponent<ItemPickup>().itemData;
        {
            itemSprite.sprite = itemData.itemSprite;
            itemNameText.text = itemData.itemName;
            itemDescriptionText.text = itemData.description;
            itemStatsText.text = $"+{itemData.itemValue} {itemData.itemType}";
            itemWeightText.text = $"+{itemData.itemWeight} Weight";
        }
    }

    public void PickupItem()
    {
        InventoryManager.Instance.TryAddItem(itemGO);
        ClosePanel();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}

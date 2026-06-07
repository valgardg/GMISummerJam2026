using UnityEngine;

public enum ItemType
{
    Consumable,
    Warmth,
    Entertainment
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("Identity")]
    public string itemName;
    [TextArea] public string description;
    public Sprite itemSprite;

    [Header("Gameplay")]
    public ItemType itemType;
    public int itemValue;
    public float itemWeight;

}
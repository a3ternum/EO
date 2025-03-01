using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    private EnumComparer EnumComparer;


    public enum EquipmentType
    {
        BodyArmour,
        Gloves,
        Boots,
        Helmet,
        Weapon,
        Ring,
        Amulet,
        Belt
    }

    public Image image;
    [SerializeField]
    private EquipmentType equipmentType;

    public void OnDrop(PointerEventData eventData) // handle logic if item is dropped in slot.
    {
        GameObject dropped = eventData.pointerDrag;
        InventoryItem inventoryItem = dropped.GetComponent<InventoryItem>();

        // implement the logic for consumable items
        if (inventoryItem.IsDraggableItem() == false) // item was never draggable so immediately return
        {
            return;
        }

        if (CanConsumeItem(inventoryItem)) // item is a consumable and cannot be dropped into equipment slot.
        {
            return;
        }

        // check for the item type and determine if it matches the equipment slot type
        bool itemsAreSameType = EnumComparer.AreEnumValuesEqual(inventoryItem.item.type, equipmentType);

        if (!itemsAreSameType) { return; }
    }


    private bool CanConsumeItem(InventoryItem inventoryItem)
    {
        return inventoryItem.item.type == Item.ItemType.Consumable;
    }

}

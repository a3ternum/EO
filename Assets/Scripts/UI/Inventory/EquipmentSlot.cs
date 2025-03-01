using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour, IDropHandler
{

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
            Debug.Log("item was not draggable");
            return;
        }

        if (CanConsumeItem(inventoryItem)) // item is a consumable and cannot be dropped into equipment slot.
        {
            Debug.Log("item is consumable");
            return;
        }

        // check for the item type and determine if it matches the equipment slot type
        bool itemsAreSameType = EnumComparer.AreEnumValuesEqual(inventoryItem.item.type, equipmentType);

        if (!itemsAreSameType)
        {
            Debug.Log("items are not the same type");
            return;
        }

        // at this point the items are the same type, we should do the following
        // 1. check if the equipment slot is empty, check if the item can be worn in that slot, if it can, equip the item
        // 2. if the equipment slot is not empty, check if it can be worn in that slot, if it can, swap the items

        if (transform.childCount == 0) // empty slot so change the parent of the item
        {
            Debug.Log("empty slot");
            // check if the item can be worn in the equipment slot
            if (!CanBeWornInSlot(inventoryItem))
            {
                Debug.Log("item cannot be worn in slot");
                inventoryItem.transform.SetParent(inventoryItem.parentAfterDrag); // return the item to its original position
                return; // exit the method
            }

            Debug.Log("item can be worn in slot");
            inventoryItem.parentAfterDrag = transform;
        }
        else // there is already an item in the slot
        {
            // check if the item can be worn in the equipment slot
            if (!CanBeWornInSlot(inventoryItem))
            {
                // move the item in the equipment slot into the parentAfterDrag slot
                transform.GetChild(0).GetComponent<InventoryItem>().parentAfterDrag = inventoryItem.parentAfterDrag; // update the parentAfterDrag of the item in the equipment slot
                transform.GetChild(0).SetParent(inventoryItem.parentAfterDrag); // move the item in the equipment slot to the parentAfterDrag slot

                inventoryItem.parentAfterDrag = transform; // return the item to its original position
                return; // exit the method
            }

        }

    }


    private bool CanConsumeItem(InventoryItem inventoryItem)
    {
        return inventoryItem.item.type == Item.ItemType.Consumable;
    }


    private bool CanBeWornInSlot(InventoryItem inventoryItem)
    {
        // this method will eventually have to check stat requirements and other things
        // for now we will just return true
        return true;
    }
}

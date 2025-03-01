using System.Collections.Generic;
using UnityEngine;

public class EnumComparer : MonoBehaviour
{
    private static readonly Dictionary<Item.ItemType, EquipmentSlot.EquipmentType> armourToItemMapping = new Dictionary<Item.ItemType, EquipmentSlot.EquipmentType>
    {
        { Item.ItemType.BodyArmour, EquipmentSlot.EquipmentType.BodyArmour},
        { Item.ItemType.Boots, EquipmentSlot.EquipmentType.Boots},
        { Item.ItemType.Gloves, EquipmentSlot.EquipmentType.Gloves},
        { Item.ItemType.Ring, EquipmentSlot.EquipmentType.Ring},
        { Item.ItemType.Amulet, EquipmentSlot.EquipmentType.Amulet},
        { Item.ItemType.Belt, EquipmentSlot.EquipmentType.Belt},
        { Item.ItemType.Weapon, EquipmentSlot.EquipmentType.Weapon},
    };

    public bool AreEnumValuesEqual(Item.ItemType itemType, EquipmentSlot.EquipmentType equipmentType)
    {
        return armourToItemMapping.TryGetValue(itemType, out var mappedItem) && mappedItem == equipmentType;
    }

}

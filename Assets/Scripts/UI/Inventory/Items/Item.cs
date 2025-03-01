using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge.Xml;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Codice.Client.Common.GameUI;

[CreateAssetMenu(fileName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    public bool onFloor;

    [Header("Only UI")]
    public bool stackable = false;
    public int maxStackSize = 4;
    public bool consumableCompatible = false;
    [Header("Both")]
    public Sprite image;
    public string itemName;
    public string itemDescription;

   
    public void UseItem(Item targetItem)
    {
        if (type == ItemType.Consumable)
        {
            Debug.Log("Consumable used");
        }
        else
        {
            Debug.Log("Item is not consumable");
        }

    }
    public enum ItemType
    {
        Weapon,
        BodyArmour,
        Gloves,
        Helmet,
        Boots,
        Ring,
        Amulet,
        Belt,
        Consumable
    }

    public enum ActionType
    {
        ApplyConsumable
    }


}



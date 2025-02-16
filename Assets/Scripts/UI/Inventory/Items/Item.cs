using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Codice.Client.BaseCommands.Merge.Xml;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Scriptable object/Item")]
public class Item : ScriptableObject
{


    [Header("Only gameplay")]

    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = false;

    [Header("Both")]
    public Sprite image;

}

public enum ItemType
{
    Weapon,
    Armor,
    Consumable
}

public enum ActionType
{
    ApplyConsumable
}

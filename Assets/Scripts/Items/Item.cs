using UnityEngine;
/// <summary>
/// this is the base class for all items in the game
/// 
/// all items will have the following shared properties
/// - name
/// - description
/// - icon sprite (inventory image) 
/// 
/// all items will have the following shared functionality
/// - all items can be picked up
/// - all items can be dragged and dropped
/// - all items can be put in an inventory
/// 
/// - if item is on the floor, it shows the name of the item in a clickable text box (when the player clicks on the item its added to the inventory)
/// - if item is in the inventory, it shows the sprite of the item in the inventory
/// 
/// </summary>
public class Item : MonoBehaviour
{
    protected string itemName;
    protected string itemDescription;
    protected Sprite itemIconSprite;


    



}

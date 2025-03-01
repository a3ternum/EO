using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class GroundItem : MonoBehaviour
{
    [Header("UI")]
    private TextMeshPro descriptionText;

    public Item item;
    public int stackSize;

    public void InitializeItem(Item newItem, int count)
    {
        item = newItem;

        if (descriptionText == null)
        {
            descriptionText = gameObject.AddComponent<TextMeshPro>();
            descriptionText.fontSize = 5;
            descriptionText.alignment = TextAlignmentOptions.Center;
        }

        descriptionText.text = item.itemDescription;
        stackSize = count;
        Canvas.ForceUpdateCanvases(); // update the canvas to get the correct bounds of the text

        // add a collider to the description text to make it clickable
        BoxCollider2D collider = descriptionText.gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(descriptionText.bounds.size.x, descriptionText.bounds.size.y);
    }

   

    private void OnMouseDown()
    {
        // check if the click is on the description text
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit.collider != null && hit.collider.gameObject == descriptionText.gameObject)
        {
            OnItemClick();
        }
    }


    private void OnItemClick()
    {
        InventoryManager.Instance.AddItem(item, stackSize); // on button click, pick up the item and add it to the inventory
        Destroy(gameObject); // destroy the gameobject after picking it up
    }

}

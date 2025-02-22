using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class GroundItem : MonoBehaviour
{
    [Header("UI")]
    private TextMeshPro descriptionText;

    public Item item;
    [HideInInspector] public int count = 1;

    public void InitializeItem(Item newItem)
    {
        item = newItem;
        Debug.Log("item description is: " + item.itemDescription);

        if (descriptionText == null)
        {
            descriptionText = gameObject.AddComponent<TextMeshPro>();
            descriptionText.fontSize = 5;
            descriptionText.alignment = TextAlignmentOptions.Center;
        }

        descriptionText.text = item.itemDescription;

        Canvas.ForceUpdateCanvases(); // update the canvas to get the correct bounds of the text

        // add a collider to the description text to make it clickable
        BoxCollider2D collider = descriptionText.gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(descriptionText.bounds.size.x, descriptionText.bounds.size.y);
    }


    private void OnMouseDown()
    {
        Debug.Log("entering onmousedown");
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
        InventoryManager.Instance.AddItem(item); // on button click, pick up the item and add it to the inventory
        Destroy(gameObject); // destroy the gameobject after picking it up
    }

}

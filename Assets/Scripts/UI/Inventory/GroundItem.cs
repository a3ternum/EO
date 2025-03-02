using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class GroundItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI")]
    private TextMeshPro descriptionText;
    private GroundItemTextBox groundTextBox;

    private BoxCollider2D textBoxCollider;
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
        textBoxCollider = descriptionText.gameObject.AddComponent<BoxCollider2D>();

        textBoxCollider.size = new Vector2(descriptionText.bounds.size.x, descriptionText.bounds.size.y);

        // instantiate the ground item text box prefab
        groundTextBox = Instantiate(Resources.Load<GroundItemTextBox>("Inventory/GroundItemTextBox"), transform, false);
        groundTextBox.transform.SetParent(transform); // set the groundTextBox as a child of the ground item
        // move the ground item text box to the top of the ground item
        groundTextBox.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);

        groundTextBox.itemName.text = item.itemName;
        groundTextBox.itemStats.text = item.itemStats;
        Debug.Log("ground item stats: " + groundTextBox.itemStats);
        groundTextBox.gameObject.SetActive(false); // hide the ground item text box
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("in OnPointerEnter on ground item");
        Debug.Log("groundtextbox: " + groundTextBox);
        // activate the groundTextBox prefab of the item
        if (groundTextBox != null)
        {
            groundTextBox.gameObject.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // deactivate the groundTextBox prefab of the item
        if (groundTextBox != null)
        {
            groundTextBox.gameObject.SetActive(false);
        }

    }
}

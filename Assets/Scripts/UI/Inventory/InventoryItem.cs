using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private bool isDraggable = false;
    private bool RMBdragging = false;
    [Header("UI")]
    public Image image;
    public TextMeshProUGUI countText;


    public Item item;
    [HideInInspector] public int count = 1;
    [HideInInspector] public Transform parentAfterDrag;


    public void InitializeItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
        refreshCount();
    }

    public void refreshCount()
    {
        countText.text = count.ToString();
        bool textActive = count > 1;
        countText.gameObject.SetActive(textActive);
    }



    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            isDraggable = true;
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.parent.parent);
            transform.SetAsLastSibling();

            image.raycastTarget = false;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (CanConsumeItem())
            {
                isDraggable = true;
                RMBdragging = true;


                parentAfterDrag = transform.parent;
                transform.SetParent(transform.parent.parent);
                transform.SetAsLastSibling();

                image.raycastTarget = false;
            }
            else
            {
                isDraggable = false;
            }
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            return;
        }
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDraggable)
        {
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            transform.SetParent(parentAfterDrag);
            image.raycastTarget = true;
        }

    }

    private bool CanConsumeItem()
    {
        return item.type == ItemType.Consumable;
    }

    public bool IsRightMouseDragging()
    {
        return RMBdragging;
    }

}

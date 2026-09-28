using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InventoryHand : MonoBehaviour
{
    public static InventoryHand i;
    InventoryItem heldObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = Input.mousePosition;
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;
            if (Input.GetMouseButtonUp(0))
            {               
                ReleaseItem(pointerEventData);
            }
            else
            {
                GridSquare hit = GetGridSquare(pointerEventData);
                if (hit != null)
                {
                    hit.ItemGridHighlight(heldObject.width, heldObject.height);
                }                                                                    
            }
        }
    }
    public GridSquare GetGridSquare(PointerEventData eventData)
    {
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);
        if (result.Count > 0)
        {
            return result[0].gameObject.GetComponent<GridSquare>();
        }
        else
        {
            return null;
        }
    }

    public void ReleaseItem(PointerEventData eventData)
    {
        GridSquare hit = GetGridSquare(eventData);
        if (hit != null)
        {
            if (hit.IsValidLocation(heldObject.width, heldObject.height))
            {
                hit.DropItem();
                heldObject.Drop();
            }
            else
            {
                heldObject.CancelMove();
            }
        }
        else
        {
            heldObject.CancelMove();
        }

    }
    public void PickUpItem(InventoryItem item)
    {
        heldObject = item;
    }
    public void DropHeldItem()
    {
        heldObject = null;
    }

    public InventoryItem GetHeldItem()
    {
        return heldObject;
    }
}

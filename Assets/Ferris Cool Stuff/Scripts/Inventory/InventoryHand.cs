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
                ItemSquare hit = GetGridSquare(pointerEventData);
                if (hit != null)
                {
                    if (hit.IsValidLocation(heldObject))
                    {
                        hit.ItemGridApplyState(heldObject.width, heldObject.height, GridSquareState.highlight);
                    }
                    else
                    {
                        hit.ItemGridApplyState(heldObject.width, heldObject.height, GridSquareState.invalid);
                    }
                }                                                                    
            }
        }
    }
    public ItemSquare GetGridSquare(PointerEventData eventData)
    {
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);
        if (result.Count > 0)
        {
            foreach(RaycastResult r in result)
            {
                ItemSquare gridSquare = r.gameObject.GetComponent<ItemSquare>();
                if(gridSquare != null)
                {
                    return gridSquare;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }

    public void ReleaseItem(PointerEventData eventData)
    {
        ItemSquare hit = GetGridSquare(eventData);
        if (hit != null)
        {
            if (hit.IsValidLocation(heldObject))
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
            // drop item into world
            InventoryItemDropper.instance.Drop(heldObject);
            Destroy(heldObject.gameObject);
        }
        InventoryGrid.instance.ClearHighlights();
    }
    public void PickUpItem(InventoryItem item)
    {
        heldObject = item;
        item.SetGridSquareSize(InventoryGrid.instance.gridItemSize);
        InventoryGrid.instance.RemoveItem(heldObject);
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

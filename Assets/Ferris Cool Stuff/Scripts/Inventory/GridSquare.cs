using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GridSquare : ItemSquare
{
    public int row;
    public int col;
    public InventoryGrid inventory;
    
    public override void DropItem()
    {
        if (InventoryHand.i.GetHeldItem() != null)
        {
           inventory.AddItemAtLocation(InventoryHand.i.GetHeldItem(), col, row);
        }
    } 
    int GetTargetColumn(InventoryItem item)
    {
        Vector3 target = (gameObject.transform.position - item.transform.position) - (gameObject.transform.position - Input.mousePosition);
        return col - (int)(target.x / inventory.gridItemSize);
    }
    int GetTargetRow(InventoryItem item)
    {
        Vector3 target = (gameObject.transform.position - item.transform.position) - (gameObject.transform.position - Input.mousePosition);
        return row - (int)(target.y / inventory.gridItemSize);
    }
    public override ItemSquare GetOriginSquare(InventoryItem item)
    {
        Vector3 target = (gameObject.transform.position - item.transform.position) - (gameObject.transform.position - Input.mousePosition);
        int targetColumn = GetTargetColumn(item);
        int targetRow = GetTargetRow(item);
        return inventory.GetInventoryGridSquare(targetColumn, targetRow);
    }
    GridSquare GetTargetSquare(InventoryItem item)
    {      
        int targetColumn = GetTargetColumn(item);
        int targetRow = GetTargetRow(item);
        return inventory.GetInventoryGridSquare(targetColumn, targetRow);
    }

    public override bool IsValidLocation(InventoryItem item)
    {
        return inventory.IsValidLocation(col, row, item.width, item.height);
    }

    public override void ItemGridApplyState(int width, int height, GridSquareState toApply)
    {
        inventory.ItemGridApplyState(col, row, width, height, toApply);
    }
}

public enum GridSquareState
{
    normal,
    highlight,
    invalid,
}

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

using UnityEngine;

public class EquipmentSlot : ItemSquare
{
    public EquipmentType equipmentType;
    InventoryItem equippedItem;
    public override void DropItem()
    {
        if (InventoryHand.i.GetHeldItem() != null)
        {
            if (equippedItem != null)
            {
                if (!InventoryGrid.instance.AddItem(equippedItem))
                {
                    InventoryItemDropper.instance.Drop(equippedItem);
                    Destroy(equippedItem.gameObject);
                }
            }

            equippedItem = InventoryHand.i.GetHeldItem();
            equippedItem.transform.position = transform.position;
        }
    }

    public override bool IsValidLocation(InventoryItem item)
    {
        return item.itemData.equipmentType == equipmentType;
    }

    public override void ItemGridApplyState(int width, int height, GridSquareState toApply)
    {
        SetState(toApply);
    }
}

public enum EquipmentType
{
    Head,
    Chest,
    Hands,
    Legs,
    Eyes
}

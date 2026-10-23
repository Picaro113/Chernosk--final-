using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : ItemSquare, IPointerExitHandler
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
            RectTransform rect = GetComponent<RectTransform>();
            equippedItem.SetSize(rect.sizeDelta.x *  rect.localScale.x, rect.sizeDelta.y * rect.localScale.y);
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

    public void OnPointerExit(PointerEventData eventData)
    {
        SetState(GridSquareState.normal);
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

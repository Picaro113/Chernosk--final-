using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int amount;

    public void PickUp()
    {
        EquipmentManager.instance.PickupAmmo(this);
    }

    public void Drop()
    {
        EquipmentManager.instance.DropAmmo(this);
    }
}

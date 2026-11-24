using UnityEngine;
using System.Collections.Generic;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;
    public List<Ammo> heldAmmo;

    public void Start()
    {
        instance = this;
    }

    public void ChangeWeapon(InventoryItem item)
    {
        if (item != null)
        {
            GameObject spawned = Instantiate(item.itemData.worldPrefab);
            WeaponManager.Instance.PickupWeapon(spawned);
            Debug.Log("Gun Equipped");
        }
        else
        {
            WeaponManager.Instance.UnequipCurrentWeapom();
        }
    }

    public void PickupAmmo(Ammo ammo)
    {
        heldAmmo.Add(ammo);
    }

    public void DropAmmo(Ammo ammo)
    {
        heldAmmo.Remove(ammo);
    }

    public int GetCurrentAmmo()
    {
        int currentAmmo = 0;
        foreach (Ammo ammo in heldAmmo)
            currentAmmo += ammo.amount;
        return currentAmmo;
    }

    public void ConsumeAmmo(int amount)
    {
        foreach (Ammo ammo in heldAmmo)
        {
            if (ammo.amount >= amount)
            {
                ammo.amount -= amount;
                return;
            }
            else
            {
                amount -= ammo.amount;
                ammo.amount = 0;
            }
        }
    }
}

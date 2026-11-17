using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
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
}

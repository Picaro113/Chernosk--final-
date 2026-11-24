using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Gun gunHoveredOver = null;

    public AmmoBox hoveredAmmoBox = null;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHitByRaycast = hit.transform.gameObject;

            // switch component to PickUp after playtest
            if (objectHitByRaycast.GetComponent<PickUp>())
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    

                    PickUp(objectHitByRaycast.GetComponent<PickUp>());
                   // for later 
                   // WeaponManager.Instance.PickupWeapon(objectHitByRaycast.gameObject);
                }
            }
            
            else
            {
                if (gunHoveredOver)
                {
                    gunHoveredOver.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
    void PickUp(PickUp item)
    {
        GameObject spawned = Instantiate(item.itemData.inventoryPrefab);
        InventoryItem inventoryItem = spawned.GetComponent<InventoryItem>();
        Ammo pickedUpAmmo = item.GetComponent<Ammo>();
        if (InventoryGrid.instance.AddItem(inventoryItem)) 
        {
            if (pickedUpAmmo != null)
            {
                spawned.GetComponent<Ammo>().amount = pickedUpAmmo.amount;
            }
            Destroy(item.gameObject);
        }
    }
}


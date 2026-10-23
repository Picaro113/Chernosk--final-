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

            if (objectHitByRaycast.GetComponent<PickUp>())
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                  
                    PickUp(objectHitByRaycast.GetComponent<PickUp>());
                }
            }
            
            else
            {
                if (gunHoveredOver)
                {
                    gunHoveredOver.GetComponent<Outline>().enabled = false;
                }
            }
            // ammo box 
            if (objectHitByRaycast.GetComponent<AmmoBox>())
            {
                hoveredAmmoBox = objectHitByRaycast.gameObject.GetComponent<AmmoBox>();
                hoveredAmmoBox.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.Instance.PickupAmmo(hoveredAmmoBox);
                    Destroy(objectHitByRaycast.gameObject);
                }
            }

            else
            {
                if (hoveredAmmoBox)
                {
                    hoveredAmmoBox.GetComponent<Outline>().enabled = false;
                }
            }
        }
    }
    void PickUp(PickUp item)
    {
        GameObject spawned = Instantiate(item.itemData.inventoryPrefab);
        if (InventoryGrid.instance.AddItem(spawned.GetComponent<InventoryItem>())) 
        {
            Destroy(item.gameObject);
        }
    }
}


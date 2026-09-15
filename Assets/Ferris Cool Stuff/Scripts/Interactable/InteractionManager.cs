using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager Instance { get; set; }

    public Gun gunHoveredOver = null;

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

            if (objectHitByRaycast.GetComponent<Gun>())
            {
                gunHoveredOver = objectHitByRaycast.gameObject.GetComponent<Gun>();
                gunHoveredOver.GetComponent<Outline>().enabled = true;

                if (Input.GetKeyDown(KeyCode.F))
                {
                    WeaponManager.instance.PickupWeapon(objectHitByRaycast.gameObject);
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
}


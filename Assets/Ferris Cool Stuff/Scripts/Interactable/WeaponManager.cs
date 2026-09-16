using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance { get; set; }

    public List<GameObject> weaponsSlots;

    public GameObject activeWeaponSlot;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        activeWeaponSlot = weaponsSlots[0];
    }

    private void Update()
    {
        foreach (GameObject weapon in weaponsSlots)
        {
            if (weapon == activeWeaponSlot)
            {
                weapon.SetActive(true);
            }
            else
            {
                weapon.SetActive(false);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchActiveSlot(0);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchActiveSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchActiveSlot(2);
        }
    }

    public void PickupWeapon(GameObject weaponEquipped)
    {
        AddWeaponIntoActiveSlot(weaponEquipped);
    }

    private void AddWeaponIntoActiveSlot(GameObject weaponEquipped)
    {

        DropCurrentWeapon(weaponEquipped);
        weaponEquipped.transform.SetParent(activeWeaponSlot.transform, false);

        Gun gun = weaponEquipped.GetComponent<Gun>();

        weaponEquipped.transform.localPosition = new Vector3(gun.spawnPosition.x, gun.spawnPosition.y, gun.spawnPosition.z);
        weaponEquipped.transform.localRotation = Quaternion.Euler(gun.spawnRotation.x, gun.spawnRotation.y, gun.spawnRotation.z);

        gun.isActiveWeapon = true;
        //gun.animator.enabled = true;
    }

    private void DropCurrentWeapon(GameObject weaponEquipped)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            var weaponToDrop = activeWeaponSlot.transform.GetChild(0).gameObject;

            weaponToDrop.GetComponent<Gun>().isActiveWeapon = false;
           // weaponToDrop.GetComponent<Gun>().animator.enabled = false;

            weaponToDrop.transform.SetParent(weaponEquipped.transform.parent);
            weaponToDrop.transform.localPosition = weaponEquipped.transform.localPosition;
            weaponToDrop.transform.localRotation = weaponEquipped.transform.localRotation;
        }
    }


    public void SwitchActiveSlot(int slotNumber)
    {
        if (activeWeaponSlot.transform.childCount > 0)
        {
            Gun currentGun = activeWeaponSlot.transform.GetChild(0).GetComponent<Gun>();
        }

        activeWeaponSlot = weaponsSlots[slotNumber];

        if (activeWeaponSlot.transform.childCount > 0)
        {
            Gun newGun = activeWeaponSlot.transform.GetChild(0).GetComponent<Gun>();
            newGun.isActiveWeapon = true;
        }
    }
    //  i will figure this out later 
    //public void DropWeaponOnGround(GameObject weaponToDrop)
    //{
    //    if (Input.GetKeyDown(KeyCode.G) && activeWeaponSlot.transform.childCount > 0)
    //    {        
    //        weaponToDrop.GetComponent<Gun>().isActiveWeapon = false;
    //        weaponToDrop.GetComponent<Rigidbody>().freezeRotation = false;
    //        weaponToDrop.GetComponent<Rigidbody>().isKinematic = true;
    //        // weaponToDrop.GetComponent<Gun>().animator.enabled = false;
    //        weaponToDrop.transform.SetParent(null);
    //    }
    }

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Gun[] guns;
    public int weaponIndex;

    private void Start()
    {
        guns = GetComponentsInChildren<Gun>();
        foreach(Gun gun in guns)
        {
            gun.gameObject.SetActive(false);
            NextWeapon();
        }
    }
    public void NextWeapon()
    {
        if (guns[weaponIndex].reloading == false)
        {

            // deactivate the current weapon
            guns[weaponIndex].gameObject.SetActive(false);
            // move to the next weapon in the array
            weaponIndex = (weaponIndex + 1) % guns.Length;
            // activate the new weapon
            guns[weaponIndex].gameObject.SetActive(true);
        }

    }

    public void PrevWeapon()
    {
        if (guns[weaponIndex].reloading == false)
        {
            // deactivate the current weapon
            guns[weaponIndex].gameObject.SetActive(false);

            // move to the previous weapon in the array
            weaponIndex = (weaponIndex + guns.Length - 1) % guns.Length;
            // activate the new weapon
            guns[weaponIndex].gameObject.SetActive(true);
        }
    }

    public Sprite GetCurrentWeaponSprite()
    {
        return guns[weaponIndex].GetComponent<SpriteRenderer>().sprite;
    }

    public void SwapWeapon(Gun newGun)
    {
        // find the index of the current weapon in the array
        int currentIndex = Array.IndexOf(guns, guns[weaponIndex]);

        // deactivate the current weapon
        guns[weaponIndex].gameObject.SetActive(false);

        // activate the new weapon
        newGun.gameObject.SetActive(true);

        // replace the current weapon with the new weapon in the array
        guns[currentIndex] = newGun;

        // set the new weapon as the current weapon
        weaponIndex = currentIndex;
    }

    public void AddWeapon(Gun newGun)
    {
        // if the inventory is full (i.e. there are already 9 weapons)
        if (guns.Length == 9)
        {
            // swap the current weapon with the new weapon
            SwapWeapon(newGun);
        }
        else
        {
            // add the new weapon to the end of the array
            Gun[] newGuns = new Gun[guns.Length + 1];
            for (int i = 0; i < guns.Length; i++)
            {
                newGuns[i] = guns[i];
            }
            newGuns[guns.Length] = newGun;
            guns = newGuns;
        }
    }

    public void GetGunFromPrefab(GameObject gunObject)
    {
        AddWeapon(gunObject.GetComponent<Gun>());
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCanvas : MonoBehaviour
{
    public Image weaponImage;
    public GameObject bulletHolder;

    public void ChangeWeaponSprite(Sprite sprite)
    {
        weaponImage.sprite = sprite;
    }
}

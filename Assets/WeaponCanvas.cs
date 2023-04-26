using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCanvas : MonoBehaviour
{
    public Image weaponImage;
    public GameObject[] bullets;

    public void ChangeWeaponSprite(Sprite sprite)
    {
        weaponImage.sprite = sprite;
    }

    public void UpdateBulletCountSprites(int bulletAmount)
    {
        for (int i = 0; i < bulletAmount; i++)
        {
            if (i < bullets.Length) // Check if index is within array bounds
            {
                bullets[i].SetActive(true);
            }
        }

        for (int i = bulletAmount; i < bullets.Length; i++)
        {
            if (i < bullets.Length) // Check if index is within array bounds
            {
                bullets[i].SetActive(false);
            }
        }
    }
}

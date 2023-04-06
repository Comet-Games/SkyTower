using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    private TopDownMovement player;
    public int health;
    public int numOfHearts;

    public int shieldAmount;
    public int numOfShields;

    public Image[] hearts;
    public Image[] shields;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public Sprite shield;
    public Sprite nul;

    private void Start()
    {
        player = GetComponent<TopDownMovement>();
    }

    private void Update()
    {
        health = player.health;
        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        shieldAmount = player.shield;
        for(int i = 0; i <shields.Length; i++)
        {
            if(i < shieldAmount)
            {
                shields[i].sprite = shield;
            }
            else
            {
                shields[i].sprite = nul;
            }
            if (i < numOfShields)
            {
                shields[i].enabled = true;
            }
            else
            {
                shields[i].enabled = false;
            }
        }
    }
}

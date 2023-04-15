using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Transform healthBar;
    public Text healthbarText;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.localScale = new Vector3(0.2f, 1, 1);
    }

    public void SetSize(float sizeNormalized)
    {
        healthBar.localScale = new Vector3(sizeNormalized, 1, 1);
    }

    public void SetColour(Color color)
    {
        healthBar.GetComponentInChildren<Image>().color = color;
    }

    public void SetName(string Bossname)
    {
        healthbarText.text = Bossname;
    }
}

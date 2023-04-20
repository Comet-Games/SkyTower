using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int doorNum;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(doorNum == 1)
            {
                Application.Quit();
            }
            if(doorNum == 2)
            {
                SceneManager.LoadScene("EnemyTester");
            }
            if(doorNum == 3)
            {
                SceneManager.LoadScene("Settings");
            }
        }
    }
}

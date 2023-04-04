using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] doors;
    public bool openDoors;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            openDoors = !openDoors;
        }

        if (openDoors)
        {
            foreach (GameObject door in doors)
            {
                OpenDoors curdoor = door.GetComponent<OpenDoors>();
                curdoor.OpenTheDoors();
            }
        }
        else if(openDoors == false)
        {
            foreach (GameObject door in doors)
            {
                OpenDoors curdoor = door.GetComponent<OpenDoors>();
                curdoor.CloseTheDoors();
            }
        }
    }

}

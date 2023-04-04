using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public Animator[] animators;
    public GameObject doorCollider;

    void OnTriggerEnter2D(Collider2D col)
    {
        //Opens the door if the player walks on it
        OpenTheDoors();
    }

    public void OpenTheDoors()
    {
        //open the doors
        foreach (Animator anim in animators)
        {
            anim.Play("Base Layer.OpenDoor");
        }

        //de-activate this object
        gameObject.SetActive(false);
        doorCollider.SetActive(false);
    }
    public void CloseTheDoors()
    {
        //close the doors
        foreach (Animator anim in animators)
        {
            anim.Play("Base Layer.CloseDoor");
        }

        //re-activate this object
        gameObject.SetActive(true);
        doorCollider.SetActive(true);
    }
}

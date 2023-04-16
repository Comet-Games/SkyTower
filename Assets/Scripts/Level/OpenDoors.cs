using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoors : MonoBehaviour
{
    public Animator[] animators;
    public GameObject doorCollider;

    private bool isOpen = false;
    private bool canClose = true;
    private bool isLocked = false; // add a new variable to keep track if the doors are locked

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Opens the door if the player walks on it and the doors are not locked
            if (!isOpen && !isLocked)
            {
                OpenTheDoors();
                StartCoroutine(CloseAfterDelay());
            }
        }
    }

    IEnumerator CloseAfterDelay()
    {
        canClose = false; // Set to false to prevent immediate closing
        yield return new WaitForSeconds(0.3f); // Wait 
        canClose = true; // Set back to true so the door can be closed
    }

    public void OpenTheDoors()
    {
        //open the doors
        foreach (Animator anim in animators)
        {
            anim.Play("Base Layer.OpenDoor");
        }

        //de-activate this object
        isOpen = true;
        doorCollider.SetActive(false);
    }

    public void CloseTheDoors()
    {
        if (canClose) // Check if door can be closed
        {
            //close the doors
            foreach (Animator anim in animators)
            {
                anim.Play("Base Layer.CloseDoor");
            }

            //reactivate the object and the door collider
            isOpen = false;
            doorCollider.SetActive(true);
        }
    }

    public void LockDoors()
    {
        isLocked = true; // set the doors to be locked
    }

    public void UnlockDoors()
    {
        isLocked = false; // set the doors to be unlocked
    }
}
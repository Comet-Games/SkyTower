using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Corridor : MonoBehaviour
{
    public Transform entrance;
    public Transform exit;
    public TilemapCollider2D colliderObj;

    public Vector3 exitPosition;
    public Quaternion exitRotation;

    private void Start()
    {
        //exitPosition = GetExitPos(exit);
        //exitRotation = GetExitRot(exit);
    }

    /*public Vector3 GetExitPos(Transform exit)
    {
        Vector3 position = exit.position;

        return position;
    }

    public Quaternion GetExitRot(Transform exit)
    {
        Quaternion rotation = exit.rotation;

        return rotation;
    }*/
}

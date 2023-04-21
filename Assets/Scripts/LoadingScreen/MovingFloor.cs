using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFloor : MonoBehaviour
{
    public Transform[] floors;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var floor in floors)
        {
            floor.Translate(-Vector3.right * speed * Time.deltaTime);
            if (floor.localPosition.x <= -30)
            {
                floor.position += new Vector3(60, 0, 0);
            }
        }
    }
}

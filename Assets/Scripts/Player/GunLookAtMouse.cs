using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLookAtMouse : MonoBehaviour
{
    public SpriteRenderer gunSpriteRenderer;
    public Transform playerTransform;
    public float gunRadius = 1.0f;
    public float gunSpeed = 10.0f;

    private void Awake()
    {
        playerTransform = GetComponentInParent<TopDownMovement>().transform;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector3 direction = mouseWorldPosition - playerTransform.position;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, Time.deltaTime * gunSpeed);
        Vector3 gunPosition = playerTransform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0) * gunRadius;
        transform.position = gunPosition;

        direction = mouseWorldPosition - gunPosition;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (mouseWorldPosition.x < playerTransform.position.x)
        {
            gunSpriteRenderer.flipY = true;
        }
        else
        {
            gunSpriteRenderer.flipY = false;
        }
    }

    void Start()
    {
        transform.parent = playerTransform;
    }
}

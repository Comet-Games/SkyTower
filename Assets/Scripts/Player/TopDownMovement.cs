using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Cursor = UnityEngine.Cursor;

public class TopDownMovement : MonoBehaviour
{
    [Header("References")]
    public Animator animator;
    public Rigidbody2D body;
    public Transform mouseObj;
    public Inventory inventory;
    public WeaponCanvas weaponCanvas;

    [Header("Health")]
    public int health = 5;
    public int shield = 1;
    public bool canTakeDamage;
    public float healthTimer;
    private float Htimer;
    public float blankRadius;

    [Header("Movement")]
    public float runSpeed = 20.0f;
    float horizontal;
    float vertical;
    bool canMove = true;
    public bool isDodging;

    [Header("Aiming")]
    Vector2 mousePos;
    public Vector2 mousePosition;
    public Vector2 mouseDistance;
    public Camera cam;

    [Header("Weapon Stuff")]
    public GameObject weaponObj;
    public float AimSpeed;

    [Header("Audio Stuff")]
    public bool holdingWeapon = false;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dodgeSound;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        Cursor.visible = false;
        inventory = GetComponent<Inventory>();
        weaponCanvas.ChangeWeaponSprite(inventory.GetCurrentWeaponSprite());
    }

    void Update()
    {
        LookAtMouse();
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);

        if (canMove)
        {
            if (horizontal != 0)
            {
                animator.SetBool("Moving", true);
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    StartCoroutine(Dodge());
                }
            }
            else if (vertical != 0)
            {
                animator.SetBool("Moving", true);
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    StartCoroutine(Dodge());
                }
            }
            else
            {
                animator.SetBool("Moving", false);
            }
        }
        animator.SetBool("Weaponless", !holdingWeapon);
        if(canTakeDamage == false)
        {
            Htimer += Time.deltaTime;
            if(Htimer > healthTimer)
            {
                Htimer = 0;
                canTakeDamage = true;
            }
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            inventory.PrevWeapon();
            weaponCanvas.ChangeWeaponSprite(inventory.GetCurrentWeaponSprite());
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            inventory.NextWeapon();
            weaponCanvas.ChangeWeaponSprite(inventory.GetCurrentWeaponSprite());
            
        }
    }

    private void FixedUpdate()
    {
        if(canMove == true)
        {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        }
        else if(canMove == false)
        {
            animator.SetBool("Moving", false);
        }
    }

    void LookAtMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseObj.position = mousePos;
        mousePosition = mouseObj.localPosition;

        animator.SetFloat("MouseHorizontal", mousePosition.x);
        animator.SetFloat("MouseVertical", mousePosition.y);

        //Weapon Stuff
        /*Vector2 Mouse2WeaponDirection = mouseObj.position - weaponObj.transform.position;
        float angle = Mathf.Atan2(Mouse2WeaponDirection.y, Mouse2WeaponDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        weaponObj.transform.rotation = Quaternion.Slerp(weaponObj.transform.rotation, rotation, AimSpeed * Time.deltaTime);*/

    }

    public void FallInPit(Vector2 respawnPos)
    {
        canMove = false;
        body.velocity = new Vector2(0, 0);
        animator.SetTrigger("Falling");
        StartCoroutine(RespawnFromPit(respawnPos));
    }

    IEnumerator RespawnFromPit(Vector2 respawnPos)
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = respawnPos;
        canMove = true;
        Debug.Log("Respawned from pit");
    }

    IEnumerator Dodge()
    {
        canMove = false;
        isDodging = true;
        audioSource.PlayOneShot(dodgeSound);
        animator.SetTrigger("Dodge");
        yield return new WaitForSeconds(0.5f);
        canMove = true;
        isDodging = false;
    }

    public void TakeDamage(int amount)
    {

        if (canTakeDamage)
        {        
            if (shield > 0)
            {
                shield--;
                Debug.Log("Shield Just Got Hit, Shield is now: " + shield);
                if (shield <= 0)
                {
                    Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, blankRadius);
                    foreach (Collider2D hitCollider in hitColliders)
                    {
                        if(hitCollider.tag == "Bullet")
                        {
                            Destroy(hitCollider.gameObject);
                        }
                    }
                    Debug.Log("Players Shield Just Broke");
                    shield = 0;
                }
            }
            else
            {
                health = health - amount;
                Debug.Log("Player Just Got Hit, Health is now: " + health);
                if (health <= 0)
                {
                    Debug.Log("The Player Just Died");
                    SceneManager.LoadScene(0);
                }
            }
            canTakeDamage = false;
        }
    }

    public void PickupWeapon(Gun weapon)
    {
        inventory.AddWeapon(weapon);
    }

    public void UpdateBullets(int amount)
    {
        weaponCanvas.UpdateBulletCountSprites(amount);
    }
}

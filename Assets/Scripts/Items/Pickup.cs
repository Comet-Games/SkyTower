using UnityEngine;
using Random = UnityEngine.Random;

public class Pickup : MonoBehaviour
{
    //public bool weapon;
    private bool inRange;
    private GameObject player;
    public GunData gunData;
    public GameObject gunPrefab;
    // Start is called before the first frame update
    void Start()
    {
        LoadGunData();
        GetComponent<SpriteRenderer>().sprite = gunData.weaponSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && player != null)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                gunPrefab.GetComponent<Gun>().gunData = gunData;
                GameObject gun = Instantiate(gunPrefab, player.transform);
                gun.SetActive(false);
                player.GetComponent<Inventory>().GetGunFromPrefab(gun);
                Destroy(gameObject);
            }
        }
    }

    private void LoadGunData()
    {
        // Load all GunData objects from the Resources/GunData folder
        Object[] loadedObjects = Resources.LoadAll("GunData", typeof(GunData));

        // Select a random GunData object from the loaded objects
        int randomIndex = UnityEngine.Random.Range(0, loadedObjects.Length);
        GunData randomGunData = (GunData)loadedObjects[randomIndex];

        // Assign the selected GunData object to the gunData variable
        gunData = randomGunData;
        Debug.Log(randomGunData.gunName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player = other.gameObject;
            inRange = true; 
            Debug.Log("Player in range");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            Debug.Log("Player out of range");
        }
    }
}

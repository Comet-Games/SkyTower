using UnityEngine;

public class Pickup : MonoBehaviour
{
    //public bool weapon;
    private bool inRange;
    private GameObject player;
    public GunData gunData;
    public GameObject gunPrefab;
    public Material outlineMat;
    // Start is called before the first frame update
    void Start()
    {
        LoadGunData();
        GetComponent<SpriteRenderer>().sprite = gunData.weaponSprite;
    }

    private void Awake()
    {
        outlineMat = GetComponent<SpriteRenderer>().material;
        outlineMat.SetFloat("_OutlineThickness", 0);
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
            outlineMat.SetFloat("_OutlineThickness", 2);
            player = other.gameObject;
            inRange = true;
            player.GetComponent<TopDownMovement>().ActivateInteractableText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            outlineMat.SetFloat("_OutlineThickness", 0);
            inRange = false;
            player.GetComponent<TopDownMovement>().DeactivateInteractableText();
        }
    }
}

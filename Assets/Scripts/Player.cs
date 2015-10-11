using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    // Object Static Variables
    private static int _maxHealth = 100;
    private static int _maxAmmo = 250;
    public static int maxHealth
    {
        get { return _maxHealth; }
    }
    public static int maxAmmo
    {
        get { return _maxAmmo; }
    }

    // Reference Variables
    private CharacterController control
    {
        get { return GetComponent<CharacterController>(); }
    }
    private Transform cam
    {
        get { return Camera.main.transform; }
    }

    // Public Reference Variables
    public Transform weaponSlot
    {
        get { return cam.GetChild(0); }
    }
    public bool hasKey
    {
        get { return gotKey; }
    }
    public List<string> hasItems
    {
        get { return gotItems; }
    }

    // Object Variables
    private Vector3 moveDirection;
    private Vector3 camRotation;
    private int curHealth = _maxHealth;
    private int curAmmo;
    private List<string> gotItems = new List<string>(4);
    private bool gotWeapon;
    private bool gotKey;

    // Events
    public delegate void HealthUpdateHandler(int curHealth);
    public static event HealthUpdateHandler HealthUpdate;
    public delegate void AmmoUpdateHandler(int curAmmo);
    public static event AmmoUpdateHandler AmmoUpdate;

    // Customizeable Variables
    public GameObject riflePrefab;
    public GameObject feedbackPrefab;
    public Transform bulletOrigin;    
    public float speed = 6;
    public float jumpSpeed = 1.5f;
    public float gravity = 0.5f;
    [Range(10, 200)]
    public int shootRange = 100;
    [Range(-45, -15)]
    public int minAngle = -30;
    [Range(30, 80)]
    public int maxAngle = 45;
    [Range(50, 500)]
    public int sensitivity = 200;
    [Range(5, 50)]
    public int healAmount = 25;
    [Range(20, 100)]
    public int ammoAmount = 40;
    
    // MonoBehaviour Functions
    private void Update()
    {
        Shoot();
        Rotate();
        Move();        
    }

    /// <summary>
    /// Controls camera rotation oriented by mouse position
    /// </summary>
    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("MouseX"));

        camRotation.x -= Input.GetAxis("MouseY") * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        cam.localEulerAngles = camRotation;
    }

    /// <summary>
    /// Controls movement with CharacterController
    /// </summary>
    private void Move()
    {
        if (control.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetButtonDown("Jump"))
                moveDirection.y = jumpSpeed;
        }

        moveDirection.y -= gravity;
        control.Move(moveDirection * speed * Time.deltaTime);
    }

    /// <summary>
    /// Raycasts to the target position, and sends a message to the object if hit
    /// </summary>
    private void Shoot()
    {
        if (gotWeapon && curAmmo > 0 && Input.GetButtonDown("Fire"))
        {
            // Displaying Shoot Feedback
            curAmmo--;
            AmmoUpdate(curAmmo);
            camRotation.x -= 2;
            GameObject gunFlare = Instantiate(feedbackPrefab, bulletOrigin.position, Quaternion.identity) as GameObject;
            gunFlare.transform.SetParent(bulletOrigin);
            Destroy(gunFlare, 0.1f);
            // Checking if the shot hit 
            Vector3 target = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, shootRange));
            Vector3 dir = target - cam.position;
            Ray ray = new Ray(cam.position, dir.normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, shootRange))
            {
                hit.collider.SendMessage("TakeDamage", SendMessageOptions.DontRequireReceiver);
                GameObject hitFlare = Instantiate(feedbackPrefab, hit.point, Quaternion.identity) as GameObject;
                Destroy(hitFlare, 0.2f);
            }
        }
    }

    /// <summary>
    /// Takes a given amount of damage
    /// </summary>
    /// <param name="amount">The amount of damage to take</param>
    private void TakeDamage(int amount)
    {
        curHealth -= amount;
        HealthUpdate(curHealth);
        if (curHealth <= 0)
        {
            // Die
            Application.LoadLevel(0);
        }
    }

    /// <summary>
    /// Recovers the defaul amount of damage
    /// </summary>
    private void RecoverHealth()
    {
        if (curHealth == _maxHealth)
            return;

        curHealth += healAmount;
        if (curHealth > _maxHealth)
            curHealth = _maxHealth;

        HealthUpdate(curHealth);
    }

    /// <summary>
    /// Collects a given amount of ammunition
    /// </summary>
    /// <param name="amount">The amount of ammo to pick</param>
    private void TakeAmmo(int amount)
    {
        curAmmo += amount;
        if (curAmmo > _maxAmmo)
            curAmmo = _maxAmmo;

        AmmoUpdate(curAmmo);
    }

    /// <summary>
    /// Gets the rifle and equips it
    /// </summary>
    private void GetRifle()
    {
        GameObject rifle = Instantiate(riflePrefab, weaponSlot.position, Quaternion.identity) as GameObject;
        rifle.transform.SetParent(weaponSlot);        
        rifle.transform.localScale = Vector3.one * 0.2f;
        rifle.transform.localRotation = Quaternion.identity;
        rifle.transform.localPosition = Vector3.zero;
        gotWeapon = true;
        TakeAmmo(20);
    }

    /// <summary>
    /// Gets an item and process its required logic
    /// </summary>
    /// <param name="itemName">The item to get</param>
    private void GetItem(string itemName)
    {
        switch (itemName)
        {
            case "Key":
                gotKey = true;
                break;
            case "Rifle":
                GetRifle();
                break;
            case "Health":
                RecoverHealth();
                break;
            case "Ammo":
                TakeAmmo(ammoAmount);
                break;
            default:
                gotItems.Add(itemName);
                break;
        }
    }
}

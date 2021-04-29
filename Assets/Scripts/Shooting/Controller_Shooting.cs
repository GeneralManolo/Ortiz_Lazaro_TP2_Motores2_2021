using UnityEngine;

public class Controller_Shooting : MonoBehaviour
{
    //makes the shooting mechanic work
    public delegate void Shooting();
    public event Shooting OnShooting;
    public static Ammo ammo;
    public static int ammunition;
    public static Controller_Shooting _ShootingPlayer;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject cannonPrefab;
    public GameObject bumeranPrefab;
    public float bulletForce = 20f;
    private bool started = false;

    private void Awake()
    {
        if (_ShootingPlayer == null)
        {
            //Ask if the player can shoot
            _ShootingPlayer = this.gameObject.GetComponent<Controller_Shooting>();
            Debug.Log("Shooting es nulo");
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if (_ShootingPlayer == null)
        {
            //will call this controller
            _ShootingPlayer = this.gameObject.GetComponent<Controller_Shooting>();
        }

        Restart._Restart.OnRestart += Reset;
        started = true;
        ammo = Ammo.Bumeran;
        ammunition = 1; //sets the boomeran ammo
    }

    private void OnEnable()
    {
        if (started)
            Restart._Restart.OnRestart += Reset;
    }

    private void Reset()
    {
        ammo = Ammo.Bumeran;
        ammunition = 1;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //When i hit the fire bottom, it will call this functions
            Shoot(); //allows the bullet to actually be fired
            CheckAmmo(); //It keeps track of my bullets and subtracts them when I shoot.
        }
    }

    private void CheckAmmo()
    {
        if (ammunition <= 0)
        {
            //turn the bullets back to normal.
            ammo = Ammo.Normal;
        }
    }

    private void Shoot()
    {
        if (OnShooting != null)
        {
            OnShooting();
        }
        if (ammo == Ammo.Normal)
        {
            //this selects the force and the gameobject that represents the normal ammo.
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            //the normal ammo exist everytime i don't have a powerup equipped.
        }
        else if (ammo == Ammo.Shotgun)
        {
            //when i grab the shotgun powerup, the bullets will change to match the shotgun's ammo. It even selects the corresponding prefab
            Rigidbody rb;
            for (float i = -0.2f; i < 0.2f; i += 0.1f)
            {
                rb = null;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                rb = bullet.GetComponent<Rigidbody>();
                rb.AddForce(new Vector3(firePoint.forward.x + i, firePoint.forward.y, firePoint.forward.z + i) * bulletForce, ForceMode.Impulse);
                //Same as normal bullets but they spread.
            }
            ammunition--; //this time i have limited ammo so the ammo count will reduce.
        }
        else if (ammo == Ammo.Cannon)
        {
            //same as shotgun. The bullets will change when i grab the respective power up
            GameObject bullet = Instantiate(cannonPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            //the bullets are bigger now.
            ammunition--; //the ammo reduces as well. Is limited
        }
        else if (ammo == Ammo.Bumeran)
        {
            //Same as the normal bullets and the shotgun.
            GameObject bullet = Instantiate(bumeranPrefab, firePoint.position, firePoint.rotation);
            Controller_Bumeran bm = bullet.GetComponent<Controller_Bumeran>();
            bm.startPos = firePoint.position;
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            ammunition--; //is limited
        }
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}

public enum Ammo
{
    //this is the types of ammo i have.
    Normal,
    Shotgun,
    Cannon,
    Bumeran
}

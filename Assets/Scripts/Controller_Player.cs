using UnityEngine;

public class Controller_Player : MonoBehaviour
{
    //The player can't work witiout this.
    public Camera cam;
    private Rigidbody rb;
    private Renderer render;
    public static Controller_Player _Player; //Only one player can use this Controller
    private Vector3 movement;
    private Vector3 mousePos;
    internal Vector3 shootAngle;
    private Vector3 startPos;
    private bool started = false;
    public float speed = 5;

    private void Start()
    {
        if (_Player == null)
        {
            _Player = this.gameObject.GetComponent<Controller_Player>(); //I make sure that whoever has this controller is a player
                                                                         //and does not interfere with another.
        }
        startPos = this.transform.position;
        rb = GetComponent<Rigidbody>(); //physics
        render = GetComponent<Renderer>();
        Restart._Restart.OnRestart += Reset;
        started = true;
        Controller_Shooting._ShootingPlayer.OnShooting += Shoot; //makes the shooting mechanic possible
    }

    private void OnEnable()
    {
        if (started)
            Restart._Restart.OnRestart += Reset;
    }

    private void Reset()
    {
        this.transform.position = startPos;
    }

    private void Update()
    {
        //makes the camera follow the playeer
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public virtual void FixedUpdate()
    {
        //without this the player can't move
        Movement();
    }

    private void Movement()
    {
        //allows the player to move and look at the direction of my clicks
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        transform.LookAt(new Vector3(mousePos.x, 1, mousePos.z));
    }

    public Vector3 GetLastAngle()
    {
        //a lot of ifs that assures me that the Gameobject is going to face the direction it is going.
        if (Input.GetKey(KeyCode.W))
        {
            shootAngle = Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            shootAngle = Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            shootAngle = Vector3.back;
        }
        if (Input.GetKey(KeyCode.D))
        {
            shootAngle = Vector3.right;
        }
        return shootAngle;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyProjectile"))
        {
            //If i touch the enemy or any enemy bullet i lose.
            gameObject.SetActive(false);
            Controller_Hud.gameOver = true;
        }
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            //If i touch a powerup, it gives me its benefits
            int rnd = UnityEngine.Random.Range(1, 3);
            //a list that choses randomly wich powerup i get.
            if (rnd == 1)
            {
                Controller_Shooting.ammo = Ammo.Shotgun;
                Controller_Shooting.ammunition = 5;
            }
            else if (rnd == 2)
            {
                Controller_Shooting.ammo = Ammo.Cannon;
                Controller_Shooting.ammunition = 5;
            }
            else
            {
                Controller_Shooting.ammo = Ammo.Bumeran;
                Controller_Shooting.ammunition = 1;
            }
            Destroy(collision.gameObject); //destroys the powerup after collecting it
        }

        if (collision.gameObject.CompareTag("Bumeran"))
        {
            //only one boomerang exist and, if i touch it, it destroys itself without making me lose.
            Controller_Shooting.ammo = Ammo.Bumeran;
            Controller_Shooting.ammunition = 1;
            Destroy(collision.gameObject);
        }
    }

    void OnDisable()
    {
        Controller_Shooting._ShootingPlayer.OnShooting -= Shoot;
        Restart._Restart.OnRestart -= Reset;
    }

    private void Shoot()
    {
        if (Controller_Shooting.ammo == Ammo.Cannon)
        {
            //Determines how fast the bullets moves.
            rb.AddForce(this.transform.forward * -4f, ForceMode.Impulse);
        }
    }
}

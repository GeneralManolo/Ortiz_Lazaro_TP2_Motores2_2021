using UnityEngine;

public class Controller_Bumeran : MonoBehaviour
{
    //Allows the boomerang to work.
    private Controller_Player parent;
    private Rigidbody rb;
    private CapsuleCollider collider;
    private Vector3 direction;
    public Vector3 startPos;
    public float maxDistance;
    public float bumeranSpeed;
    private float travelDistance;
    private float colliderTimer = 0.07f;
    private bool going;

    void Start()
    {
        parent = Controller_Player._Player; //Boomerang inherits from player.
        rb = GetComponent<Rigidbody>();
        Restart._Restart.OnRestart += Reset;
        collider = GetComponent<CapsuleCollider>(); //The boomeran has a capsule collider
        collider.enabled = false;
        going = true;
    }

    private void Reset()
    {
        Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        Rotate();
        if (going)
        {
            //When the boomerang is shot, it will travel a certain distance.
            travelDistance = (startPos - transform.position).magnitude;
            if (travelDistance > maxDistance)
            {
                //The boomerang has to stop sometimes. That is what this function does.
                CheckDirection();
            }
        }
        else
        {
            //Makes the boomerang return to the player
            ReturnToPlayer();
        }
    }

    void Update()
    {
        colliderTimer -= Time.deltaTime;
        if (colliderTimer < 0)
        {
            //allows the boomerang to have a collider so it can destroy enemies
            collider.enabled = true;
        }
        if (going)
        {
            //determines how long the boomerang travel is
            travelDistance = (startPos - transform.position).magnitude;
        }
    }

    private void CheckDirection()
    {
        //makes sure that the boomerang endeed it travel and it will check if the player is around.
        going = false;
        rb.velocity = Vector3.zero;
        if (Controller_Player._Player != null)
        {
            direction = -(this.transform.localPosition - parent.transform.localPosition).normalized;
        }
    }

    private void Rotate()
    {
        //Makes the boomerang rotates all the time
        transform.Rotate(new Vector3(10, 0, 0));
    }

    private void ReturnToPlayer()
    {
        rb.AddForce(direction * bumeranSpeed);
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}

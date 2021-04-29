using UnityEngine;

public class Projectile : MonoBehaviour
{
    //Allows me to make the bullet dessapear after a certain limit
    public float xLimit = 30;
    public float yLimit = 20;

    private void Start()
    {
        Restart._Restart.OnRestart += Reset;
    }

    private void Reset()
    {
        Destroy(this.gameObject);
    }

    virtual public void Update()
    {
        //i want to check the bullet limits all the time
        CheckLimits();
    }

    internal virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("PowerUp"))
        {
            //when the bullets touch a wall or a powerup, it destroys itself.
            Destroy(this.gameObject);
        }
    }

    internal virtual void CheckLimits()
    {
        if (this.transform.position.x > xLimit)
        {
            Destroy(this.gameObject);
        }
        if (this.transform.position.x < -xLimit)
        {
            Destroy(this.gameObject);
        }
        if (this.transform.position.z > yLimit)
        {
            Destroy(this.gameObject);
        }
        if (this.transform.position.z < -yLimit)
        {
            Destroy(this.gameObject);
        }
        //all of this IFs are conditions for the bullet to destroy itself.
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}

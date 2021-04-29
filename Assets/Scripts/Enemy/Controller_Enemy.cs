using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Controller_Enemy : MonoBehaviour
{
    //needed to make the enemy behaviour work.
    public static int numPatroler;
    internal GameObject player;
    internal NavMeshAgent agent;
    internal Renderer render;
    internal Vector3 destination;
    public float patrolDistance = 5;
    public float destinationTime = 4;
    public float enemySpeed;

    void Start()
    {
        render = GetComponent<Renderer>();
        Restart._Restart.OnRestart += Reset;
        destination = new Vector3(UnityEngine.Random.Range(-10, 12), 1, UnityEngine.Random.Range(-12, 9));
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player"); //i want the enemy to chase the player
    }

    public void Reset()
    {
        Destroy(this.gameObject);
    }

    internal virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            //the enemy destroys when a projectile hits it.
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Controller_Hud.points++; //also add points for doing so
        }
        if (collision.gameObject.CompareTag("CannonBall"))
        {
            //same as projectiles, but with the canonball
            Destroy(this.gameObject);
            Controller_Hud.points++; //also add points for the kill
        }
        if (collision.gameObject.CompareTag("Bumeran"))
        {
            //same with projectiles but with the boomerang.
            Destroy(this.gameObject);
            Controller_Hud.points++; //also add points for the kill
        }
    }

    private void OnDestroy()
    {
        Instantiator.enemies.Remove(this);
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}

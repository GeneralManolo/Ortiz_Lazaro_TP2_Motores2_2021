using System.Collections.Generic;
using UnityEngine;

public class Controller_Instantiator : MonoBehaviour
{
    //I will need this to make the enemies work. Is important to add a list, so the game can choose wich one to spawn.
    public List<GameObject> enemies;
    public GameObject instantiatePos;
    public float timer = 7;
    private float time = 0;
    private int multiplier = 20;

    void Update()
    {
        //Every tick this functions are called.
        timer -= Time.deltaTime;
        SpawnEnemies();
        ChangeVelocity();
    }

    private void ChangeVelocity()
    {
        //It makes the enemies faster
        time += Time.deltaTime;
        if (time > multiplier)
        {
            multiplier *= 2;
        }
    }

    private void SpawnEnemies()
    {
        if (timer <= 0) //decides when the enemies should spawn. I don't want the entire screen to be full of enemies.
        {
            float offsetX = instantiatePos.transform.position.x; //enemies spawn point
            int rnd = UnityEngine.Random.Range(0, enemies.Count); //chooses a random enemy from the list
            for (int i = 0; i < 5; i++)
            {
                //This for is for spawning a centain amount of enemies. 
                offsetX = offsetX + 4;
                Vector3 transform = new Vector3(offsetX, instantiatePos.transform.position.y, instantiatePos.transform.position.z);
                Instantiate(enemies[rnd], transform, Quaternion.identity);
            }
            timer = 7;
        }
    }
}

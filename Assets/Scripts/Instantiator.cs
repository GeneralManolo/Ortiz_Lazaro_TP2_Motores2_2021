using System.Collections.Generic;
using UnityEngine;

public class Instantiator : MonoBehaviour
{
    //Needed to stablish where everything is gonna appear.
    public static List<Controller_Enemy> enemies;
    public List<GameObject> enemy;
    public List<GameObject> positions;
    public GameObject powerUp;
    private GameObject powerUpInstance;
    private float initialWaveDuration, initialAumentedWaveDuration, initialPowerUpTime;
    public int wave = 1; //A counter that counts the number of waves
    public float waveDuration = 5, aumentedWaveDuration = 3, powerUpTime = 10;

    private void Start()
    {
        enemies = new List<Controller_Enemy>(); //Grabs enemies from the list in the controller_enemy
        initialWaveDuration = waveDuration; //Stablish how long the wave will last.
        initialAumentedWaveDuration = aumentedWaveDuration; //Waves after wave 1 will last longer.
        initialPowerUpTime = powerUpTime; //Allows me to make the powerups appears.
        Restart._Restart.OnRestart += Reset;
        SpawnEnemies();
    }

    private void Reset()
    {
        waveDuration = initialWaveDuration;
        aumentedWaveDuration = initialAumentedWaveDuration;
        powerUpTime = initialPowerUpTime;
        wave = 1;
        if (powerUpInstance != null)
            Destroy(powerUpInstance);
        foreach (Controller_Enemy c in enemies)
        {
            c.Reset();
        }
        SpawnEnemies();
    }

    private void Update()
    {
        //Power up spawns and enemy spawn are dicted by delta time.
        waveDuration -= Time.deltaTime;
        powerUpTime -= Time.deltaTime;
        if (powerUpTime < 0)
        {
            SpawnPowerUp();
        }
        if (waveDuration < 0)
        {
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        if (!Controller_Hud.gameOver)
        {
            int enemiesCount = wave * 2;
            for (int i = 0; i < enemiesCount; i++)
            {
                int random = UnityEngine.Random.Range(0, positions.Count); //for everyenemy chooses a random position.
                GameObject enemyInstance = Instantiate(enemy[UnityEngine.Random.Range(0, enemy.Count)], positions[random].transform.position, Quaternion.identity); //spawns an enemy
                enemies.Add(enemyInstance.GetComponent<Controller_Enemy>()); //add enemies to the list.
            }
            aumentedWaveDuration += 0.3f;
            waveDuration = aumentedWaveDuration;
            wave++;
        }
    }

    private void SpawnPowerUp()
    {
        //spawns an powerup and destroys it if i din't touch it after a while.
        Vector3 randomizer = new Vector3(UnityEngine.Random.Range(-7, 7), 1, UnityEngine.Random.Range(-7, 7)); //makes the powerup appear near the player
        powerUpInstance = Instantiate(powerUp, randomizer, Quaternion.identity);
        Destroy(powerUpInstance, 10);
        powerUpTime = 20;
    }

    private void OnDisable()
    {
        Restart._Restart.OnRestart -= Reset;
    }
}

using UnityEngine;
using UnityEngine.UI;

public class Controller_Hud : MonoBehaviour
{
    //i'm gonna need this things for everyrhing to work in the hud.
    public static bool gameOver;
    public static int points;
    private Ammo ammo;
    public Text gameOverText;
    public Text pointsText;
    public Text powerUpText;

    void Start()
    {
        //This valeus assure me that every game is a new game, with everything at 0. 
        Restart._Restart.OnRestart += Reset;
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        points = 0;
    }

    private void Reset()
    {
        //This allows me to reset tthe socre and the lose conditions, as well as making the HUD do not interfere.
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
        points = 0;
    }

    void Update()
    {
        if (gameOver)
            //This that will happen when i lose
        {
            Time.timeScale = 0;
            gameOverText.text = "Game Over";
            gameOverText.gameObject.SetActive(true);
        }

        switch (Controller_Shooting.ammo)
        {
            //The hud detects wich wepon i'm using and displays its name and its ammo on the screen.
            case Ammo.Normal:
                powerUpText.text = "Gun: Normal - Ammo:∞";
                break;
            case Ammo.Shotgun:
                powerUpText.text = "Gun: Shotgun - Ammo:" + Controller_Shooting.ammunition.ToString();
                break;
            case Ammo.Cannon:
                powerUpText.text = "Gun: Cannon - Ammo:" + Controller_Shooting.ammunition.ToString();
                break;
            case Ammo.Bumeran:
                powerUpText.text = "Gun: Bumeran - Ammo:" + Controller_Shooting.ammunition.ToString();
                break;
        }
        //just the score.
        pointsText.text = "Score: " + points.ToString();
    }

    private void OnDisable()
    {
        //This just makes the restart activates when i restart.
        Restart._Restart.OnRestart -= Reset;
    }
}

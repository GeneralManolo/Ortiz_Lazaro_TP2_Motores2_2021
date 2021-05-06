using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    //allows me to configure how slowmotion will occur.
    public float slowDownFactor = 0.05f; //how much is the slow down
    public float slowDownLength = 3f;

    private void Update()
    {

        //gives the slow motion a limit and sets the time back to normal after 3 seconds.
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

    }

    public void activateSlowMotion()
    {
        {

            Time.timeScale = slowDownFactor;

        }

    }

}

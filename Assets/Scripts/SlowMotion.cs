using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    //allows me to configure how slowmotion will occur.
    public float slowDownFactor = 0.05f; //how much is the slow down
    public float slowDownLegth = 3f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {

            activateSlowMotion();

        }
    }

    void activateSlowMotion()
    {
        {
            Time.timeScale = slowDownFactor;
        }

    }

}

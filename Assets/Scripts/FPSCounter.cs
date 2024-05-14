using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private bool isCounting = false;
    private float totalTime = 0.0f;
    private uint frameCount = 0;
    private float averageFPS = 0.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (!isCounting)
            {
                // Start counting
                isCounting = true;
                totalTime = 0.0f;
                frameCount = 0;
                averageFPS = 0.0f;
                Debug.Log("FPS counter started.");
            }
            else
            {
                // Stop counting
                isCounting = false;
                averageFPS = frameCount / totalTime;
                Debug.Log("FPS counter stopped. Average FPS: " + averageFPS);
            }
        }

        if (isCounting)
        {
            totalTime += Time.deltaTime;
            frameCount++;
        }
    }
}

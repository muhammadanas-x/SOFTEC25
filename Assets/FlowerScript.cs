using UnityEngine;

public class FlowerScript : MonoBehaviour
{
    void Update()
    {
        // Sinusoidal oscillation between -45 and 45 degrees
        float angle = Mathf.Sin(Time.time * 2f) * 45f;

        // Apply the rotation only on Z axis
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}

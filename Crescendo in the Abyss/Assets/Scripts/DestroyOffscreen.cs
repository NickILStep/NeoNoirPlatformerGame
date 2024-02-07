using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    public float offset = 2.0f; // Offset to ensure the platform is fully offscreen before being destroyed

    void Update()
    {
        // Check if the platform's position is below the bottom of the screen
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - offset)
        {
            Destroy(gameObject); // Destroy the platform
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Drag your platform prefab here in the inspector
    public float spawnRate = 0.5f; // Time in seconds between each spawn
    public float platformMinLength = 2.0f; // Minimum length of the platform
    public float platformMaxLength = 5.0f; // Maximum length of the platform
    public Transform cameraPos;

    private float timer; // Timer to keep track of spawning

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlatform();
        timer = spawnRate; // Initialize the timer
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnPlatform();
            timer = spawnRate; // Reset the timer
        }
    }

    void SpawnPlatform()
    {
        // Calculate the spawn position just above the top of the screen
        float spawnY = Camera.main.orthographicSize + transform.position.y + cameraPos.position.y + 5;
        float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);

        // Instantiate the platform at the spawn position
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Randomly scale the platform's length
        float platformLength = Random.Range(platformMinLength, platformMaxLength);
        newPlatform.transform.localScale = new Vector3(platformLength, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);
    }
}

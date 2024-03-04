using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Drag your platform prefab here in the inspector
    private float heightToSpawn;
    public float platformMinLength = 2.0f; // Minimum length of the platform
    public float platformMaxLength = 5.0f; // Maximum length of the platform
    public Transform cameraPos;
    private Vector3 prevPlatformPos = new Vector3(0, 3.35f, 0);

    private float timer; // Timer to keep track of spawning

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlatform();
        heightToSpawn = cameraPos.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float difference = cameraPos.position.y - heightToSpawn;

        if (difference >= 5.0f)
        {
            SpawnPlatform();
            heightToSpawn = cameraPos.position.y;
        }
    }

    void SpawnPlatform()
    {
        // Calculate the spawn position just above the top of the screen
        Vector3 spawnPosition = newPlatformPos();
        prevPlatformPos = spawnPosition;
        // Instantiate the platform at the spawn position
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);

        // Randomly scale the platform's length
        float platformLength = Random.Range(platformMinLength, platformMaxLength);
        newPlatform.transform.localScale = new Vector3(platformLength, newPlatform.transform.localScale.y, newPlatform.transform.localScale.z);
    }

    Vector3 newPlatformPos()
    {
        float platformPosDifference;
        float spawnY, spawnX;
        do
        {
            spawnY = Camera.main.orthographicSize + transform.position.y + cameraPos.position.y + 6;
            spawnX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);

            platformPosDifference = System.Math.Abs(prevPlatformPos.x - spawnX);
        } while (platformPosDifference > 15.0f);

        Debug.Log(platformPosDifference);
        return new Vector3(spawnX, spawnY, 0);
    }
}

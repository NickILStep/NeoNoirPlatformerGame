using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Drag your platform prefab here in the inspector
    private float heightToSpawn;
    //public float platformMinLength = 2.0f; // Minimum length of the platform
    //public float platformMaxLength = 5.0f; // Maximum length of the platform
    private Object[] platformSprites;
    private float[] lengths = { 1.0f, 2.0f, 4.0f, 8.0f };
    public Transform cameraPos;
    private Vector3 prevPlatformPos = new Vector3(0, 3.35f, 0);
    private float screenEdge = 12.89f; 

    private float timer; // Timer to keep track of spawning

    // Start is called before the first frame update
    void Start()
    {
        platformSprites = Resources.LoadAll("Platforms");
        SpawnPlatform();
        heightToSpawn = cameraPos.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraPos != null)
        {
            float difference = cameraPos.position.y - heightToSpawn;
            if (difference >= 5.0f)
            {
                SpawnPlatform();
                heightToSpawn = cameraPos.position.y;
            }
        }
    }

    void SpawnPlatform()
    {
        // Calculate the spawn position just above the top of the screen
        int randIndex = Random.Range(0, 4);
        Debug.Log("Rand index is: " + randIndex.ToString());
        float platformLength = lengths[randIndex];
        Vector3 spawnPosition = newPlatformPos(platformLength);
        prevPlatformPos = spawnPosition;
        // Instantiate the platform at the spawn position
        Sprite sprite = platformSprites[randIndex * 2 + 1] as Sprite;
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        SpriteRenderer renderer = newPlatform.GetComponent<SpriteRenderer>();
        BoxCollider2D collider = newPlatform.GetComponent<BoxCollider2D>();
        collider.size = new Vector2(platformLength, collider.size.y);
        renderer.sprite = sprite;
        newPlatform.layer = 6;
    }

    Vector3 newPlatformPos(float length)
    {
        float platformPosDifference;
        float spawnY, spawnX;
        do
        {
            spawnY = Camera.main.orthographicSize + transform.position.y + cameraPos.position.y + 6;
            spawnX = Random.Range(-screenEdge + (length / 2), screenEdge - (length / 2));

            platformPosDifference = System.Math.Abs(prevPlatformPos.x - spawnX);
        } while (platformPosDifference > 12.0f);

        //Debug.Log(platformPosDifference);
        return new Vector3(spawnX, spawnY, 0);
    }
}

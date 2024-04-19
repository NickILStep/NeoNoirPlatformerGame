using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    [System.Serializable]
    public class SpriteTier
    {
        public float heightThreshold; // Height at which this tier becomes active
        public int minIndex; // Minimum sprite index for this tier
        public int maxIndex; // Maximum sprite index (exclusive) for this tier
    }

    public GameObject platformPrefab;
    public PlatformAssetPack platformAssetPack;
    private float heightToSpawn;
    //public float platformMinLength = 2.0f; // Minimum length of the platform
    //public float platformMaxLength = 5.0f; // Maximum length of the platform
    private Object[] platformSprites;
    private float[] lengths = { 1.0f, 2.0f, 4.0f, 8.0f };
    public Transform cameraPos;
    private Vector3 prevPlatformPos = new Vector3(0, 3.35f, 0);
    private float screenEdge = 12.89f;
    public List<SpriteTier> tiers = new List<SpriteTier>();
    private int currentTierIndex = 0; // Tracks the current tier based on progression




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
        // Update the current tier index based on the camera's height
        for (int i = 0; i < tiers.Count; i++)
        {
            if (cameraPos.position.y >= tiers[i].heightThreshold)
            {
                currentTierIndex = i;
            }
            else
            {
                break; // Stop checking once we find the current tier
            }
        }

        // Get the current tier based on the updated currentTierIndex
        SpriteTier currentTier = tiers[currentTierIndex];

        // Adjust the index range based on the current tier
        int randIndex = Random.Range(currentTier.minIndex, Mathf.Min(currentTier.maxIndex, platformAssetPack.sprites.Length));
        Debug.Log("Rand index is: " + randIndex);

        // Adjust platform length selection based on tier index
        // Note: This is where we're adjusting our approach
        int lengthIndex = Mathf.Clamp(randIndex - currentTier.minIndex, 0, lengths.Length - 1);
        float platformLength = lengths[lengthIndex];

        Vector3 spawnPosition = newPlatformPos(platformLength);
        prevPlatformPos = spawnPosition;
        GameObject newPlatform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
        SpriteRenderer renderer = newPlatform.GetComponent<SpriteRenderer>();
        BoxCollider2D collider = newPlatform.GetComponent<BoxCollider2D>();

        // Assign the sprite to the renderer
        renderer.sprite = platformAssetPack.sprites[randIndex];

        // Here's where we dynamically set the collider size based on the sprite size
        // We're considering the local scale of the newPlatform to correctly size the collider
        float spriteWidth = renderer.sprite.bounds.size.x * newPlatform.transform.localScale.x;
        float spriteHeight = renderer.sprite.bounds.size.y * newPlatform.transform.localScale.y;
        collider.size = new Vector2(spriteWidth, spriteHeight);

        newPlatform.layer = 6;
    }


    Vector3 newPlatformPos(float length)
    {
        float platformPosDifference;
        float spawnY, spawnX;
        do
        {
            spawnY = Camera.main.orthographicSize + transform.position.y + cameraPos.position.y + 6;

            float adjustedEdge = screenEdge - (length);
            spawnX = Random.Range(-adjustedEdge, adjustedEdge);

            platformPosDifference = System.Math.Abs(prevPlatformPos.x - spawnX);
        } while (platformPosDifference > 11.0f);

        return new Vector3(spawnX, spawnY, 0);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    private List<string> Powerups;
    private float lastSpawnHeight;
    private float screenEdge = 12.89f;
    private float newSpawnHeight = 100f;
    void Start()
    {
        Powerups = new List<string>()
        {
            "Jetpack",
        };
        lastSpawnHeight = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y >= lastSpawnHeight + newSpawnHeight)
        {
            lastSpawnHeight = transform.position.y;
            SpawnPowerup();
        }
    }

    void SpawnPowerup()
    {
        int randomPowerup = Random.Range(0, Powerups.Count);
        string tag = Powerups[randomPowerup];

        float randomX = Random.Range(-screenEdge + 2, screenEdge - 2);
        float yPos = transform.position.y + 10;
        Vector3 spawnPos = new Vector3(randomX, yPos);
        GameObject gameObject = Instantiate(GameObject.FindGameObjectWithTag(tag), spawnPos, Quaternion.identity);
        gameObject.SetActive(true);
    }
}

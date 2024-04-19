using System;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Transform cameraPos;
    private float timeBetweenSpawns;
    private float startTimeBetweenSpawns = 20f;
    private float screenEdge = 12.89f;
    private enum Direction { Left, Right }
// Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawns = startTimeBetweenSpawns;
    }

    // Update is called once per frame
    void Update()
    {
        int tier = ((int)(cameraPos.position.y / 280)) + 1; //Counting tiers as 280 units. For every tier, enemy spawn rate goes up. Spawning starts on tier 3.
        if(tier >= 3)
        {
            timeBetweenSpawns -= Time.deltaTime;
            if (timeBetweenSpawns <= 0)
            {
                timeBetweenSpawns = startTimeBetweenSpawns / tier;
                SpawnEnemy();
            }
        }

        foreach (var obj in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (obj.transform.position.y < cameraPos.position.y - 10 && obj.transform.position.y > -11) //Leave the original alone
            {
                Destroy(obj);
            }
            else
            {
                if (Math.Abs(obj.transform.position.x) > screenEdge)
                {
                    Rigidbody2D rbx = obj.GetComponent<Rigidbody2D>();
                    rbx.AddForce(new Vector2(rbx.velocity.x * -2, 0), ForceMode2D.Impulse);
                    obj.transform.position = new Vector3(obj.transform.position.x < 5 ? -screenEdge : screenEdge, obj.transform.position.y);
                }
            }
        }
    }

    void SpawnEnemy()
    {
        float yPos = cameraPos.position.y + 10;
        float xPos = UnityEngine.Random.Range(-screenEdge + 2, screenEdge - 2);

        Vector3 spawnPos = new Vector3(xPos, yPos);
        GameObject gameObject = Instantiate(GameObject.FindGameObjectWithTag("Enemy"), spawnPos, Quaternion.identity);
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(xPos * -1, AutoScrollBackup.GetCurrentScrollSpeed());
        rb.AddForce(force, ForceMode2D.Impulse);
        gameObject.SetActive(true);
        Debug.Log("Enemy Spawned");
    }
}

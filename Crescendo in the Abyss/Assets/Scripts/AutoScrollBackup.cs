using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AutoScrollBackup : MonoBehaviour
{
    private float scrollSpeed;
    private float followScrollSpeed;
    private float positionToSpeedUp;
    public Transform player;

    void Start()
    {
        scrollSpeed = 1.5f;
        positionToSpeedUp = 20.0f;
    }

    void Update()
    {
        float speedUpAmount = 0f;
        if (transform.position.y >= positionToSpeedUp)
        {
            speedUpAmount += 0.15f;
            positionToSpeedUp += 20.0f;
        }
        scrollSpeed += speedUpAmount;
        transform.localPosition += Vector3.up * scrollSpeed * Time.deltaTime;
        if(player.position.y > transform.position.y + 5)
        {
            followScrollSpeed = scrollSpeed + (player.position.y - transform.position.y);
            transform.localPosition += Vector3.up * followScrollSpeed * Time.deltaTime;
        }
    }

    public float GetCurrentScrollSpeed()
    {
        return scrollSpeed;
    }
}

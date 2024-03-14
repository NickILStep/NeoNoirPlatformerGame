using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AutoScrollBackup : MonoBehaviour
{
    private float scrollSpeed;
    private float followScrollSpeed;
    private float positionToSpeedUp;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = 1.5f;
        positionToSpeedUp = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        float speedUpAmount = 0f;
        if (transform.position.y >= positionToSpeedUp)
        {
            speedUpAmount += 0.5f;
            positionToSpeedUp += 20.0f;
        }
        scrollSpeed += speedUpAmount;
        Debug.Log(scrollSpeed);
        transform.localPosition += Vector3.up * scrollSpeed * Time.deltaTime;
        if(player.position.y > transform.position.y + 5)
        {
            followScrollSpeed = scrollSpeed + (player.position.y - transform.position.y);
            transform.localPosition += Vector3.up * followScrollSpeed * Time.deltaTime;
        }
    }
}
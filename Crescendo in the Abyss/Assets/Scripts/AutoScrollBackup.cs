using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AutoScrollBackup : MonoBehaviour
{
    private float scrollSpeed;
    private float followScrollSpeed;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        scrollSpeed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.up * scrollSpeed * Time.deltaTime;
        if(player.position.y > transform.position.y + 3)
        {
            followScrollSpeed = scrollSpeed + (player.position.y - transform.position.y);
            transform.localPosition += Vector3.up * followScrollSpeed * Time.deltaTime;
        }
    }
}
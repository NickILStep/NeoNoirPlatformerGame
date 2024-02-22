using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    public float scrollSpeed = 1.0f; // Speed at which objects will move down

    // Update is called once per frame
    void Update()
    {
        // Move the object down continuously at scrollSpeed units per second
        //transform.localPosition += Vector3.down * scrollSpeed * Time.deltaTime;
    }
}


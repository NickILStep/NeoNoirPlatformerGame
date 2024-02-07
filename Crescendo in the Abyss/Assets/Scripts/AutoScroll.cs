using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AutoScroll : MonoBehaviour
{
    private float timeToMove = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timeToMove = timeToMove - Time.deltaTime;
        if (timeToMove <= 0.0f) 
        {
            transform.localPosition += new Vector3(0, 10, 0);
            Debug.Log("Current position: " + transform.position.y);
            timeToMove = 1.0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AutoScrollBackup : MonoBehaviour
{
    private float scrollSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += Vector3.up * scrollSpeed * Time.deltaTime;
    }
}
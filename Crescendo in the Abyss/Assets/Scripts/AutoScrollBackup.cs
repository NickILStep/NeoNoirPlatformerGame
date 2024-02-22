using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AutoScrollBackup : MonoBehaviour
{
    public float scrollSpeed = 2f;
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
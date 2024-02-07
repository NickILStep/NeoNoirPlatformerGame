using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothness;
    public Transform targetObject;
    private Vector3 initialOffset;
    private Vector3 cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        //targetObject = GameObject.Find("CameraObject");
        initialOffset = transform.position - targetObject.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraPosition = targetObject.position + initialOffset;
        transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothness * Time.fixedDeltaTime);
    }
}

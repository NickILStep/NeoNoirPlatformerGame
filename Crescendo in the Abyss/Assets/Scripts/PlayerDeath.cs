using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Transform cameraPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float cameraY = cameraPos.position.y;
        float playerY = transform.position.y;

        if(playerY < cameraY - 9.5)
        {
            Debug.Log("Player Died");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

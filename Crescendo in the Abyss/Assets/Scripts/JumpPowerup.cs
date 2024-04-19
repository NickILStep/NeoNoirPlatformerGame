using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpPowerup : MonoBehaviour
{
    public float timer;
    public float jumpTimer = 30.0f;
    public PlayerControllerCircleTest PlayerController;
    public ParticleSystem Aura;

    public bool isActive = false;
    private bool isUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        timer = jumpTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerController.isGrounded && isActive && !isUsed)
        {
            PlayerController.hasDoubleJump = true;
            isUsed = true;
        }

        if (PlayerController.isGrounded)
        {
            isUsed = false;
        }

        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                Aura.Stop();
                isActive = false;
                PlayerController.hasDoubleJump = false;
            }
        }
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.tag == "DoubleJump")
        {
            timer = jumpTimer;
            Aura.Play();
            isActive = true;
            Destroy(collision.gameObject);
        }
    }
}

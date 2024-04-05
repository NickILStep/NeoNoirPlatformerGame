using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JetpackPowerup : MonoBehaviour
{
    public Transform Camera;
    public Transform Player;
    private Rigidbody2D rb;
    private float prevCameraPos;
    public AutoScrollBackup autoScrollScript;
    public PlayerControllerCircleTest PlayerController;
    // Start is called before the first frame update
    void Start()
    {
        rb = Player.GetComponent<Rigidbody2D>();
        prevCameraPos = Camera.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float scrollSpeed = autoScrollScript.GetCurrentScrollSpeed();
        float adjustedGravityScale = PlayerController.gravityScale + scrollSpeed * 0.2f;
        if (Camera.position.y + 50 >= prevCameraPos)
        {
            Sprite sprite = transform.GetComponent<SpriteRenderer>().sprite;
        }
        if (PlayerController.startTimer)
        {
            PlayerController.timer -= Time.deltaTime;
            if(PlayerController.timer <= 0)
            {
                rb.gravityScale = adjustedGravityScale;
            }
        }
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.tag == "Jetpack")
        {
            rb.gravityScale = 0f;
            rb.AddForce(new Vector2(0f, JetpackBoost()), ForceMode2D.Impulse);
            PlayerController.startTimer = true;
            PlayerController.isGrounded = false;
            PlayerController.animator.SetBool("isJumping", true);
            PlayerController.animator.SetBool("isGrounded", false);
            Destroy(collision.gameObject);
        }
    }

    private float JetpackBoost()
    {
        return 40.0f;
    }
}

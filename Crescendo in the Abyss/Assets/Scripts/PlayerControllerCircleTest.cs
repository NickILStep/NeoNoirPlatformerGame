using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCircleTest : MonoBehaviour
{
    public float speed = 5f; // Speed of the ball movement
    public float jumpForce = 10f; // Force of the jump
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    private bool isGrounded = true; // To check if the ball is on the ground

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(); // Call the function to move the player
        Jump(); // Call the function to make the player jump
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Get left/right arrow key input
        Vector2 movement = new Vector2(moveHorizontal, 0f); // Create movement vector
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y); // Apply movement to the Rigidbody2D
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) // Check for up arrow key press and if the ball is grounded
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Add a vertical force for the jump
            isGrounded = false; // Set isGrounded to false to prevent double jumps
        }
    }

    // Detect collision with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) // Make sure your ground has a tag "Ground"
        {
            isGrounded = true; // Set isGrounded to true when colliding with the ground
        }
    }
}

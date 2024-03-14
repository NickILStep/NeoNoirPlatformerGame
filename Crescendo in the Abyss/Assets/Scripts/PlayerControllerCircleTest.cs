using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCircleTest : MonoBehaviour
{
    public float speed = 5f; // Speed of the ball movement
    public float jumpForce = 8f; // Force of the jump
    public float gravityScale = 1f; // Force of gravity for different jump heights
    public float jumpTimer = 0.5f; // Max length of jump time
    public float timer; // To keep track of jump time
    public float screenBounds = 10f; // To set the edge boundary of the screen
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    public bool isGrounded = true; // To check if the ball is on the ground
    private bool startTimer = false; // To keep track of if we're using the timer for jumping
    public Animator animator; //Used to interact with animation states

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component

        timer = jumpTimer; // Set the timer for jumping
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

        //facing right
        if(Input.GetAxis("Horizontal") > 0.01f)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = 1;
            gameObject.transform.localScale = theScale;
        }
        //facing left
        if (Input.GetAxis("Horizontal") < -0.01f)
        {
            Vector3 theScale = transform.localScale;
            theScale.x = -1;
            gameObject.transform.localScale = theScale;
        }

        animator.SetFloat("speed", Mathf.Abs(moveHorizontal)); //

        if(transform.position.x > screenBounds)
        {
            transform.position = new Vector3(screenBounds, transform.position.y, transform.position.z);
        }
        else if(transform.position.x < -screenBounds)
        {
            transform.position = new Vector3(-screenBounds, transform.position.y, transform.position.z);
        }
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.gravityScale = 0f;
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            startTimer = true;
            isGrounded = false;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.gravityScale = gravityScale;
            startTimer = false;
        }

        if (!isGrounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.gravityScale = 4 * gravityScale;
        }

        if (startTimer)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                rb.gravityScale = gravityScale;
                startTimer = false;
            }
        }
    }

        //if(rb.gravityScale < gravityScale && Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    rb.gravityScale = gravityScale;
        //}
        
        //else
        //{
        //    rb.gravityScale = 0f;

    //void Jump()
    //{
    //    if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded) // Check for up arrow key press and if the ball is grounded
    //    {
    //        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); // Add a vertical force for the jump
    //        isGrounded = false; // Set isGrounded to false to prevent double jumps
    //    }
    //}

    // Detect collision with the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach(ContactPoint2D hitPos in collision.contacts)
        {
            if (hitPos.normal.y > 0 && collision.gameObject.CompareTag("Ground")) // Make sure your ground has a tag "Ground" and is making contact with the bottom of the player
            {
                isGrounded = true; // Set isGrounded to true when colliding with the ground
                animator.SetBool("isGrounded", true);
                timer = jumpTimer;
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animator.SetBool("isGrounded", false);
        }
    }
}

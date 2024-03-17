using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCircleTest : MonoBehaviour
{
    public float startSpeed = 5f;
    private float speed; // Speed of the player movement
    public float jumpForce = 8f; // Force of the jump
    public float gravityScale = 1f; // Force of gravity for different jump heights
    public float jumpTimer = 0.5f; // Max length of jump time
    public float timer; // To keep track of jump time
    public float screenBounds = 10f; // To set the edge boundary of the screen
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    public bool isGrounded = true; // To check if the ball is on the ground
    private bool startTimer = false; // To keep track of if we're using the timer for jumping
    public Animator animator; //Used to interact with animation states
    public AutoScrollBackup autoScrollScript; //for getting the speed of the background scroll


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
        GameManager.Instance.UpdateScore(transform.position.y);
    }

    void MovePlayer()
    {
        float scrollSpeed = autoScrollScript.GetCurrentScrollSpeed();
        // Adjust speed based on scrollSpeed
        speed = startSpeed + scrollSpeed * 1.0f;

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

        animator.SetFloat("speed", Mathf.Abs(moveHorizontal)); //running animation

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
        float scrollSpeed = autoScrollScript.GetCurrentScrollSpeed();
        // Adjust jumpForce based on scrollSpeed
        float adjustedJumpForce = jumpForce + scrollSpeed * 1.0f; 

        // adjust gravityScale based on scrollSpeed for more natural jump at higher speeds
        float adjustedGravityScale = gravityScale + scrollSpeed * 0.2f; // Tweak this multiplier based on testing

        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.gravityScale = 0f; // Consider keeping this or adjusting if the initial ascent feels off
            rb.AddForce(new Vector2(0f, adjustedJumpForce), ForceMode2D.Impulse);
            startTimer = true;
            isGrounded = false;
            animator.SetBool("isJumping", true);
            animator.SetBool("isGrounded", false);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            rb.gravityScale = adjustedGravityScale;
            startTimer = false;
        }

        if (!isGrounded && Input.GetKeyDown(KeyCode.DownArrow))
        {
            // Consider how to adjust this based on scroll speed, if at all
            rb.gravityScale = 4 * adjustedGravityScale; // Adjusting the fast-fall to scale with the game's pace
            animator.SetBool("isFastFall", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isGrounded", false);
        }

        if (startTimer)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                rb.gravityScale = adjustedGravityScale;
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
                animator.SetBool("isGrounded", true); //plays grounded animation
                animator.SetBool("isJumping", false); //hopefully cycles to Idle animation
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
            animator.SetBool("isGrounded", false); //isnt grounded yet
        }
    }
}

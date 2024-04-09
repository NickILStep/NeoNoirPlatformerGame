using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerControllerCircleTest : MonoBehaviour
{
    public float startSpeed = 5f;
    private float speed; // Speed of the player movement
    public float jumpForce = 8f; // Force of the jump
    public float adjustedJumpForce;
    public float gravityScale = 1f; // Force of gravity for different jump heights
    public float jumpTimer = 0.5f; // Max length of jump time
    public float timer; // To keep track of jump time
    public float screenBounds = 10f; // To set the edge boundary of the screen
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    public bool isGrounded = true; // To check if the ball is on the ground
    public bool startTimer = false; // To keep track of if we're using the timer for jumping
    public bool isWall = false;
    public Animator animator; //Used to interact with animation states
    public AutoScrollBackup autoScrollScript; //for getting the speed of the background scroll
    public float slidingMomentum = 10f; // Sliding momentum force
    public float slipperyHeightThreshold = 250f; // Height threshold for slippery platforms
    private bool isSliding = false; // Flag to track if the player is sliding
    public bool hasDoubleJump = false;


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
        if (Input.GetAxis("Horizontal") > 0.01f)
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

        if (isSliding)
        {
            // Apply sliding momentum based on the player's facing direction
            if (transform.localScale.x > 0)
            {
                rb.AddForce(Vector2.right * slidingMomentum, ForceMode2D.Force);
            }
            else if (transform.localScale.x < 0)
            {
                rb.AddForce(Vector2.left * slidingMomentum, ForceMode2D.Force);
            }
        }

        animator.SetFloat("speed", Mathf.Abs(moveHorizontal)); //running animation

        if (transform.position.x > screenBounds)
        {
            transform.position = new Vector3(screenBounds, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -screenBounds)
        {
            transform.position = new Vector3(-screenBounds, transform.position.y, transform.position.z);
        }
    }

    void Jump()
    {
        float scrollSpeed = autoScrollScript.GetCurrentScrollSpeed();
        // Adjust jumpForce based on scrollSpeed
        adjustedJumpForce = jumpForce + scrollSpeed * 1.0f;

        // adjust gravityScale based on scrollSpeed for more natural jump at higher speeds
        float adjustedGravityScale = gravityScale + scrollSpeed * 0.2f; // Tweak this multiplier based on testing

        if ((isGrounded || hasDoubleJump) && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Mouse0)))
        {
            if (isWall || hasDoubleJump)
            {
                rb.velocity = Vector3.zero;
                animator.SetBool("isJumping", false);
                animator.SetBool("isFastFall", false);
            }
            rb.gravityScale = 0f; // Consider keeping this or adjusting if the initial ascent feels off
            rb.AddForce(new Vector2(0f, adjustedJumpForce), ForceMode2D.Impulse);
            startTimer = true;
            isGrounded = false;
            animator.SetBool("isJumping", true);
            animator.SetBool("isGrounded", false);

            if (hasDoubleJump)
            {
                hasDoubleJump = false;
            }
        }

        if (!isWall && (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Mouse1)))
        {
            rb.gravityScale = adjustedGravityScale;
            startTimer = false;
        }

        if (!isGrounded && (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.Mouse1)))
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
        foreach (ContactPoint2D hitPos in collision.contacts)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                // Top of platform
                if (hitPos.normal.y == 1)
                {
                    isGrounded = true;
                    animator.SetBool("isGrounded", true);
                    animator.SetBool("isJumping", false);
                    timer = jumpTimer;

                    // Check if the player is above the slippery height threshold
                    if (transform.position.y >= slipperyHeightThreshold)
                    {
                        isSliding = true;
                    }
                    else
                    {
                        isSliding = false;
                    }

                    break;
                }

                // Side of platform
                if (Math.Abs(hitPos.normal.x) == 1 && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Math.Abs(rb.transform.position.y - collision.transform.position.y) < .65)
                {
                    isGrounded = true; // Set isGrounded to true when colliding with the ground
                    isWall = true;
                    timer = jumpTimer;
                    rb.velocity = Vector3.zero;
                    rb.gravityScale = 0f;
                    startTimer = true;
                    animator.SetBool("isJumping", false);
                    animator.SetBool("isFastFall", false);
                    break;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isWall = false;
            isGrounded = false;
            animator.SetBool("isGrounded", false); //isnt grounded yet
        }
    }
}
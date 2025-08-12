using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D myRigidbody2D;

    [Header("Movement")]
    private float moveInput;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float dashForce = 20f;

    private bool facingRight = true;
    private bool isDashing = false;

    private bool jumpPressed = false;
    private bool dashPressed = false;

    [Header("Ground & Jump")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.2f;
    [SerializeField] private bool canJump = true;
    private bool canDash = true;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if(GameManager.Instance.IsGameStarted == false) return;

        moveInput = Input.GetAxisRaw("Horizontal");

        // Flip the sprite based on movement
        if ((moveInput > 0 && !facingRight) || (moveInput < 0 && facingRight))
        {
            Flip();
        }

        // Input flags
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jumpPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.W) && canDash)
        {
            dashPressed = true;
        }
    }

    private void FixedUpdate()
    {

        if(GameManager.Instance.IsGameStarted == false) return;

        Vector2 rayOrigin = new Vector2(transform.position.x, transform.position.y - 0.5f);
        bool isGrounded = Physics2D.Raycast(rayOrigin, Vector2.down, groundCheckDistance, groundLayer);

        if (isGrounded ) // && myRigidbody2D.linearVelocity.y <= 0 (Double jump I want to stop it then add this thing because I am not going too far in Y)
        {
            canJump = true;
            canDash = true;
        }

        // Movement
        if (!isDashing)
        {
            myRigidbody2D.linearVelocity = new Vector2(moveInput * moveSpeed, myRigidbody2D.linearVelocity.y);
        }

        if (jumpPressed)
        {
            Jump();
            jumpPressed = false;
        }

        if (dashPressed)
        {
            Dash();
            dashPressed = false;
        }
    }

    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Destroy_Object"))
    //     {
    //         Destroy(collision.gameObject);
    //     }

    //     if (collision.CompareTag("Block_Trigger"))
    //     {
    //         Debug.Log("Positions Of blocks changing");
    //         EventBus.TriggerBlockFall();
    //     }

    //     if (collision.CompareTag("Block_Object"))
    //     {
    //         Debug.Log("Player Collided with block");
    //         // GameOver here
    //     }

    //     if (collision.CompareTag("Size_Trigger"))
    //     {
    //         // Debug.Log("Size changing");
    //         EventBus.TriggerBlockSizeChange();
    //         Destroy(collision.gameObject);
    //     }

    //     if (collision.CompareTag("Spikes_Trigger"))
    //     {
    //         // Debug.Log("Spikes Triggered");
    //         EventBus.TriggerSpikesMovesFarAway();
    //         Destroy(collision.gameObject);
    //     }

    //     if (collision.CompareTag("Spikes_Trigger_2"))
    //     {
    //         // Debug.Log("Spikes Triggered");
    //         EventBus.TriggerSpikesMovesComeClose();
    //         Destroy(collision.gameObject);
    //     }

    //     if (collision.CompareTag("Spikes_Trigger_3"))
    //     {
    //         // Debug.Log("Spikes Triggered");
    //         EventBus.TriggerSpikesMovesChangePosition();
    //         Destroy(collision.gameObject);
    //     }

    //     if (collision.CompareTag("Spikes_Object"))
    //     {
    //         Debug.Log("Player died");
    //     }

    //     if (collision.CompareTag("Block_Trigger_2"))
    //     {
    //         // Debug.Log("Positions Of blocks changing");
    //         EventBus.TriggerBlockMoveAndTryFall();
    //         Destroy(collision.gameObject);
    //     }

    //     if (collision.CompareTag("Block_Trigger_3"))
    //     {
    //         // GameOver here
    //         EventBus.TriggerBlockMove();
    //         Destroy(collision.gameObject);
    //     }

    // }

    private void Jump()
    {
        myRigidbody2D.linearVelocity = new Vector2(myRigidbody2D.linearVelocity.x, jumpForce);
        canJump = false;
    }

    private void Dash()
    {
        isDashing = true;
        canDash = false;

        float dashDirection = facingRight ? 1f : -1f;
        myRigidbody2D.linearVelocity = new Vector2(dashDirection * dashForce, 0f);

        Invoke(nameof(StopDashing), 0.2f);
    }

    private void StopDashing()
    {
        isDashing = false;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}

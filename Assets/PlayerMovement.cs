using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Continuous forward motion
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        // Jump control (Mouse Click or Mobile Screen Touch)
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;

            // Play Jump Sound Effect
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.jumpSound);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Reset jump when landing on ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Handle Coin Collection safely without double-trigger errors
        if (other != null && other.CompareTag("Coin"))
        {
            // Disable collider immediately so it can't trigger twice
            other.enabled = false;

            // Play Coin Pickup Sound
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.coinSound);
            }

            // Add score and destroy coin object
            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(10);
            }

            Destroy(other.gameObject);
        }
    }
}
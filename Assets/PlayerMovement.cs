using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;             // Starting forward speed
    public float speedIncreaseRate = .05f;  // Acceleration per second
    public float maxSpeed = 15f;             // Speed cap

    [Header("Jump Settings")]
    public float baseJumpForce = 8f;         // Starting jump force at baseSpeed
    public float maxJumpForce = 14f;         // Maximum jump force at maxSpeed
    private float currentJumpForce;

    [Header("Camera Tracking")]
    public Transform mainCamera;
    public float cameraXOffset = 3f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (mainCamera == null && Camera.main != null)
        {
            mainCamera = Camera.main.transform;
        }

        currentJumpForce = baseJumpForce;
    }

    void Update()
    {
        // 1. Automatically increase speed over time up to maxSpeed
        if (moveSpeed < maxSpeed)
        {
            moveSpeed += speedIncreaseRate * Time.deltaTime;
        }

        // 2. Automatically scale jump force proportionally to current speed
        float speedRatio = (moveSpeed - 5f) / (maxSpeed - 5f); // Normalized ratio (0.0 to 1.0)
        currentJumpForce = Mathf.Lerp(baseJumpForce, maxJumpForce, speedRatio);

        // 3. Continuous forward motion
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        // 4. Move camera along X axis to track player
        if (mainCamera != null)
        {
            mainCamera.position = new Vector3(transform.position.x + cameraXOffset, mainCamera.position.y, mainCamera.position.z);
        }

        // 5. Jump control using scaled jump force
        if ((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, currentJumpForce);
            isGrounded = false;

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.jumpSound);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Coin"))
        {
            other.enabled = false;

            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.coinSound);
            }

            if (GameManager.instance != null)
            {
                GameManager.instance.AddScore(10);
            }

            Destroy(other.gameObject);
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player2DMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 7f;
    public float groundCheckDistance = 2f;
    public float coyoteTime = 0.1f;
    public float jumpCooldown = 0.3f;

    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpRequested = false;
    private bool onCooldown = false;
    private float coyoteTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ
                       | RigidbodyConstraints.FreezeRotationX
                       | RigidbodyConstraints.FreezeRotationY
                       | RigidbodyConstraints.FreezeRotationZ;
    }

void Update()
{
// ✅ Exclude the capsule's own layer from the check
    int layerMask = ~LayerMask.GetMask("Player"); // exclude Player layer
    Vector3 feetPosition = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
    bool groundedThisFrame = Physics.CheckSphere(feetPosition, 0.1f, layerMask);

    // Debug.Log("groundedThisFrame: " + groundedThisFrame + " | onCooldown: " + onCooldown);

    if (groundedThisFrame)
    {
        coyoteTimer = coyoteTime;
        onCooldown = false;
    }
    else
    {
        coyoteTimer -= Time.deltaTime;
    }

    isGrounded = coyoteTimer > 0f;

    if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !onCooldown)
    {
        jumpRequested = true;
        coyoteTimer = 0f;
        onCooldown = true;
    }
}

    void FixedUpdate()
    {
        float moveInput = 0f;
        if (Input.GetKey(KeyCode.D)) moveInput = -1f;
        if (Input.GetKey(KeyCode.A)) moveInput = 1f;

            // ✅ Flip character based on direction
    if (Input.GetKey(KeyCode.D))
        transform.localScale = new Vector3(1f, 1f, 1f);
    if (Input.GetKey(KeyCode.A))
        transform.localScale = new Vector3(-1f, 1f, 1f);

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, 0f);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }

        rb.linearVelocity = new Vector3(moveInput * moveSpeed, rb.linearVelocity.y, 0f);
    }

    void OnDrawGizmos()
{
    Gizmos.color = Color.yellow;
    Vector3 feetPosition = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
    Gizmos.DrawWireSphere(feetPosition, 0.1f);
}
}
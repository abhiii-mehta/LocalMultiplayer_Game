using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isPlayerOne = true;
    public float jumpForce = 7f;

    private Rigidbody rb;
    private Animator animator;
    private Vector3 movement;

    private bool isJumping = false;
    private float jumpTimer = 0f;
    public float jumpDuration = 0.5f;  // How long jump "feels" like it lasts

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (isPlayerOne)
        {
            if (Input.GetKey(KeyCode.A)) moveX = -1f;
            if (Input.GetKey(KeyCode.D)) moveX = 1f;
            if (Input.GetKey(KeyCode.W)) moveZ = 1f;
            if (Input.GetKey(KeyCode.S)) moveZ = -1f;
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
            if (Input.GetKey(KeyCode.UpArrow)) moveZ = 1f;
            if (Input.GetKeyDown(KeyCode.DownArrow)) moveZ = -1f;
            if (Input.GetKeyDown(KeyCode.RightControl)) Jump();
        }

        movement = new Vector3(moveX, 0f, moveZ).normalized;

        // Animate walking
        if (animator != null)
        {
            bool isMoving = movement.magnitude > 0.1f;
            animator.SetBool("IsWalking", isMoving);
        }

        // Handle jump timer
        if (isJumping)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0f)
            {
                isJumping = false;
            }
        }
        else
        {
            ApplyFakeGravity();  // Only apply gravity when not jumping
        }
    }

    private void ApplyFakeGravity()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, Vector3.down);
        if (!Physics.Raycast(ray, 0.6f))
        {
            // Player is not grounded
            transform.position += Vector3.down * 5f * Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        if (!isJumping)
        {
            animator.SetTrigger("IsJumping");
            isJumping = true;
            jumpTimer = jumpDuration;
        }
    }
    public void ForceFall()
    {
        // Do nothing here for now, OR you can trigger manual raycast fake fall
    }


}

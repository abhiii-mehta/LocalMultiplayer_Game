using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public bool isPlayerOne = true;

    private Rigidbody rb;
    private Vector3 movement;

    public float jumpForce = 5f;
    private bool isGrounded = true;

    private Animator animator;

    // Drunk mode after checkpoint2
    public bool isDrunk = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Get Animator
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
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
            if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
            if (Input.GetKey(KeyCode.UpArrow)) moveZ = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) moveZ = -1f;
            if (Input.GetKeyDown(KeyCode.RightControl) && isGrounded)
            {
                Jump();
            }
        }

        movement = new Vector3(moveX, 0f, moveZ).normalized;

        //  UPDATE Animator Parameters 
        if (animator != null)
        {
            animator.SetBool("IsWalking", movement.magnitude > 0.1f);
            animator.SetBool("IsJumping", !isGrounded);
            animator.SetBool("IsDrunk", isDrunk);
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up)
        {
            isGrounded = true;
        }
    }
}

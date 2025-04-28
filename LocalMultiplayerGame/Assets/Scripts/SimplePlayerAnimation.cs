using UnityEngine;

public class SimplePlayerAnimation : MonoBehaviour
{
    public Animation anim;

    public AnimationClip idleClip;
    public AnimationClip walkClip;
    public AnimationClip jumpClip;
    public AnimationClip drunkWalkClip;

    public bool isDrunk = false;

    private bool isGrounded = true;

    void Start()
    {
        anim = GetComponent<Animation>();
        anim.AddClip(idleClip, "Idle");
        anim.AddClip(walkClip, "Walk");
        anim.AddClip(jumpClip, "Jump");
        anim.AddClip(drunkWalkClip, "DrunkWalk");

        anim.Play("Idle");
    }

    void Update()
    {
        bool isWalking = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)
                      || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.RightControl))
        {
            Jump();
        }

        // Animation switching
        if (!isGrounded)
        {
            anim.CrossFade("Jump");
        }
        else if (isDrunk && isWalking)
        {
            anim.CrossFade("DrunkWalk");
        }
        else if (isWalking)
        {
            anim.CrossFade("Walk");
        }
        else
        {
            anim.CrossFade("Idle");
        }
    }

    void Jump()
    {
        isGrounded = false;
        // Apply jump physics if needed
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up)
        {
            isGrounded = true;
        }
    }
}

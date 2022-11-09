using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float horizontal;
    private float speed = 8f;
    private float jumpPower = 16f;
    [SerializeField] private bool isFacingRight = true;
    public bool canJump = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

   // public float wallJumpTime = .75f;
    public float wallSlideSpeed = .3f;
    // public float wallDistance = .5f;
   
    private bool isWallSliding = false;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Vector2 wallCheckSize;

    [SerializeField] float wallJumpForce = 18f;
    [SerializeField] float wallJumpDirection = -1f;
    [SerializeField] Vector2 wallJumpAngle;

    [SerializeField] RaycastHit2D wallCheckHit;
    [SerializeField] private float jumpTime;
    float mx = 0f;
    void Start()
    {
        wallJumpAngle.Normalize();
    }

    
    void Update()
    {
       
        Flip();
    }

    void Jump()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded() && canJump == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void FixedUpdate()
    {
        if(IsGrounded())
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        Jump();
        WallSlide();

        WallJump();
    }
    [SerializeField] private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    void WallSlide()
    {
        if(IsTouchingWall() && !IsGrounded() && rb.velocity.y <0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if(isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, wallSlideSpeed); 
        }
    }
    void WallJump()
    {
        if((isWallSliding || IsTouchingWall()) && canJump)
        {
            rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
            canJump = false;
        }
    }
   [SerializeField] private bool IsTouchingWall()
    {
        return Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}

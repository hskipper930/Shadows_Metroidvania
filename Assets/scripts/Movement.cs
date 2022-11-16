using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int numLogs = 0;
    [SerializeField] private float horizontal;
    private float speed = 8f;
    private float jumpPower = 5f;
    [SerializeField] private bool isFacingRight = true;
    public bool canJump = false;
    public bool candoubleJump = false;

    public Transform firePoint;
    public GameObject axePrefab;
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

    public int playerHealth = 30;
    public bool axeActive = true;
    public float axePower = 200f;
    public int activeWeapon = 1;
    void Start()
    {
        wallJumpAngle.Normalize();
    }

    
   

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boots"))
        {
            canJump = true;
            Debug.Log("You can jump now");
            Destroy(collision.gameObject);
        }
        
    }

    void Jump()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded() && canJump == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
        else if(candoubleJump)//for later!
        { candoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(0, jumpPower));
        }

        /*if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }*/
    }

    void Attack()
    {
        
        switch(activeWeapon)//going to add weapons to switch to in the future.
            { 
            case 1:     //axe
                if (axeActive == true)
                {
                    Instantiate(axePrefab, firePoint.position, firePoint.rotation);

                }
                break;
        }
    }
    private void Update()
    {
        Flip();
        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }
        if(IsGrounded())
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        Jump();
        WallSlide();

        //WallJump();
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

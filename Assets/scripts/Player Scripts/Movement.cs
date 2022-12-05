using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class Movement : MonoBehaviour
{
    public int numLogs = 0;
    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpPower = 8f;
    [SerializeField] public bool isFacingRight = true;
    [SerializeField]private bool canJump = false;
    private bool candoubleJump = false;
 

    public Transform firePoint;
    public GameObject axePrefab;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

   // public float wallJumpTime = .75f;
    private float wallSlideSpeed = .3f;
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

    [SerializeField] public GameObject spawnLocation;
    public Animator animate;
    //public CharacterController controller;


    private int playerHealth = 30;
    private bool axeActive = true;
    public float axePower = 200f;
    private int activeWeapon = 1;
    public bool isSharp = false;
    [SerializeField] private float axeCooldown = 1f;
    private float fireRate;

    [SerializeField] private TMP_Text healthText;
    [SerializeField] private GameObject gameOverPanel;
    public Transform pitfall;
    private bool gameOver = false;
    public bool hasKey = false;
    

    void Start()
    {
        wallJumpAngle.Normalize();
        healthText.text = "HP: " + playerHealth;
    }

    
   

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boots"))
        {
            canJump = true;
            Debug.Log("You can jump now");
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Water"))
        {
           transform.position = pitfall.position;
            TakeDamage(5);
        }
        if(collision.gameObject.CompareTag("Ground"))
        {
            //animate.SetBool("IsJumping", false);
        }
        if(collision.gameObject.CompareTag("Health"))
        {
            playerHealth += 10;
            healthText.text = "HP: " + playerHealth;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("AxeUpgrade"))
        {
            isSharp = true;
            Destroy(collision.gameObject);
        }

        /*if(collision.gameObject.CompareTag("RespawnPoint"))
        {
            
        }
        
       /* if(collision.gameObject.CompareTag("Log"))
        {
            numLogs++;
            Debug.Log("Logs picked up: " + numLogs);
            Destroy(collision.gameObject);
        }*/

    }
    
    public void TakeDamage(int amount)
    {
        playerHealth -= amount;
        healthText.text = "HP: " + playerHealth;
        if(playerHealth <= 0)
        {
            GameOver();
        }
    }

    void Jump()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded() && canJump == true)
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animate.SetBool("IsJumping", true);
        }
        if(Input.GetButtonUp("Jump"))
        {
            animate.SetBool("IsJumping", false);

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
                    
                    Instantiate(axePrefab, firePoint.position, transform.rotation);
                    animate.SetBool("AxeAttack", true);
                    axeActive = false;
                }
                
                break;
        }
    }
    private void Update()
    {
        if (!gameOver)
        {
            animate.SetFloat("Speed", Mathf.Abs(horizontal));
            Flip();
            if (Input.GetKeyDown(KeyCode.E))
            {

              
                
                    
                
                Attack();
            }
            if(axeActive == false)
            {
                axeCooldown -= Time.deltaTime;
                if (axeCooldown <= 0)
                {
                    axeActive = true;
                    axeCooldown = 1f;
                }
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                animate.SetBool("AxeAttack", false);

            }
            /* if(IsGrounded())
             {
                 rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
             }*/
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            Jump();
            WallSlide();

            //WallJump();

            if (transform.position.y <= -50)
            {
                GameOver();
            }
        }

        if(playerHealth > 30)
        {
            playerHealth = 30;
            healthText.text = "HP: " + playerHealth.ToString();
        }
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

    private void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }
}

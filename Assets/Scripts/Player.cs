using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField]
    private float Speed = 10f;
    [SerializeField]
    private float JumpForce = 5f;

    public Animator Anim;
    public float xInput;
    public bool isFacingRight = true;
    public Transform Character;

    public float groundCheckDistance = 1.5f;
    public bool isOnGround = false;
    public LayerMask GroundMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleCollision();
        HandleInput();
        HandleMovement();
        HandleAnimations();
        HandleFlip();
    }


    void HandleInput()
    {
        //xInput = Input.GetAxis("Horizontal");
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }
    }
    void Jump()
    {
        if (isOnGround)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
        }
    }
    void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * Speed, rb.linearVelocity.y);
    }
    void HandleAnimations()
    {
        bool isMoving = rb.linearVelocity.x != 0;
        bool isJumping=rb.linearVelocity.y !=0;
        Anim.SetBool("isMoving", isMoving);
        Anim.SetBool("isJumping", isJumping);
    }

    void HandleFlip()
    {
        if(rb.linearVelocity.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if(rb.linearVelocity.x <0 && isFacingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Character.Rotate(0, 180, 0);
    }

    void HandleCollision()
    {
        isOnGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, GroundMask);
    }

   
}

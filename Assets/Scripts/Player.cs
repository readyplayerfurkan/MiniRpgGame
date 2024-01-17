using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;
    private int _facingDir = 1;
    private bool _isFacingRight = true;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private float _xInput;

    [Header("Dash Info")] 
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    
    [Header("Collision Info")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckDistance;
    private bool _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        CollisionChecks();
        Movement();
        CheckInput();
        FlipController();
        AnimatorControllers();
        
        dashTime-= Time.deltaTime;
    }

    private void CheckInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            dashTime = dashDuration;
    }

    private void Movement()
    {
        if (dashTime > 0)
            _rb.velocity = new Vector2(_xInput * dashSpeed, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(_xInput * moveSpeed, _rb.velocity.y);
    }

    private void CollisionChecks()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void Jump()
    {
        if(_isGrounded)
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void Flip()
    {
        _facingDir = _facingDir * -1;
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if (_rb.velocity.x > 0 && !_isFacingRight)
            Flip();
        else if (_rb.velocity.x < 0 && _isFacingRight)
            Flip();
    }

    private void AnimatorControllers()
    {
        bool isMoving = _rb.velocity.x != 0;
        
        _anim.SetFloat("yVelocity", _rb.velocity.y);
        _anim.SetBool("isMoving", isMoving);
        _anim.SetBool("isGrounded", _isGrounded);
    }
}

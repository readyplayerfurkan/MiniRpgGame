using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    private int _facingDir = 1;
    private bool _isFacingRight = true;
    private float _xInput;

    [Header("Dash Info")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    private float _dashTime;
    private float _dashCooldownTimer;

    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    private bool _isGrounded;
    
    [Header("Attack Info")] 
    [SerializeField] private float comboTime = .3f;
    private float _comboTimeWindow;
    private bool _isAttacking;
    private int _comboCounter;
    
    private Animator _anim;
    private Rigidbody2D _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
        _comboTimeWindow = comboTime;
    }

    private void Update()
    {
        CollisionChecks();
        Movement();
        CheckInput();
        FlipController();
        AnimatorControllers();

        _dashCooldownTimer -= Time.deltaTime;
        _dashTime -= Time.deltaTime;
        _comboTimeWindow -= Time.deltaTime;
    }

    private void CheckInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetMouseButtonDown(0))
        {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            DashAbility();
    }

    private void StartAttackEvent()
    {
        if (!_isGrounded)
            return;
        
        if (_comboTimeWindow < 0)
            _comboCounter = 0;
            
        _isAttacking = true;
        _comboTimeWindow = comboTime;
    }

    public void AttackOver()
    {
        _isAttacking = false;

        if (_comboCounter < 2)
            _comboCounter++;
        else
            _comboCounter = 0;
    }

    private void DashAbility()
    {
        if (_dashCooldownTimer > 0 && _isAttacking) return;

        _dashTime = dashDuration;
        _dashCooldownTimer = dashCooldown;
    }

    private void Movement()
    {
        if (_isAttacking)
            _rb.velocity = Vector2.zero;
        else if (_dashTime > 0)
            _rb.velocity = new Vector2(_facingDir * dashSpeed, _rb.velocity.y);
        else
            _rb.velocity = new Vector2(_xInput * moveSpeed, _rb.velocity.y);
    }

    private void CollisionChecks()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    private void Jump()
    {
        if (_isGrounded)
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
        
        // switch (_rb.velocity.x)
        // {
        //     case > 0 when !_isFacingRight:
        //     case < 0 when _isFacingRight:
        //         Flip();
        //         break;
        // }
    }

    private void AnimatorControllers()
    {
        bool isMoving = _rb.velocity.x != 0;

        _anim.SetFloat("yVelocity", _rb.velocity.y);
        _anim.SetBool("isMoving", isMoving);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetBool("isAttacking", _isAttacking);
        _anim.SetInteger("comboCounter", _comboCounter);
    }
}
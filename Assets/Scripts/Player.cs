using UnityEngine;

public class Player : Entity
{
    [Header("Movement Variables")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    
    private float _xInput;

    [Header("Dash Info")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashCooldown;
    private float _dashTime;
    private float _dashCooldownTimer;
    
    [Header("Attack Info")] 
    [SerializeField] private float comboTime = .3f;
    private float _comboTimeWindow;
    private int _comboCounter;

    protected override void Update()
    {
        base.Update();
        
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
        if (!IsGrounded)
            return;
        
        if (_comboTimeWindow < 0)
            _comboCounter = 0;
            
        IsAttacking = true;
        _comboTimeWindow = comboTime;
    }

    public void AttackOver()
    {
        IsAttacking = false;

        if (_comboCounter < 2)
            _comboCounter++;
        else
            _comboCounter = 0;
    }

    private void DashAbility() 
    {
        if (_dashCooldownTimer > 0 || IsAttacking) return;

        _dashTime = dashDuration;
        _dashCooldownTimer = dashCooldown;
    }

    private void Movement()
    {
        if (IsAttacking)
            Rb.velocity = Vector2.zero;
        else if (_dashTime > 0)
            Rb.velocity = new Vector2(_xInput * dashSpeed, Rb.velocity.y);
        else
            Rb.velocity = new Vector2(_xInput * moveSpeed, Rb.velocity.y);
    }
    
    private void Jump()
    {
        if (IsGrounded)
            Rb.velocity = new Vector2(Rb.velocity.x, jumpForce);
    }

    private void FlipController()
    {
        if (Rb.velocity.x > 0 && !IsFacingRight)
            Flip();
        else if (Rb.velocity.x < 0 && IsFacingRight)
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
        bool isMoving = Rb.velocity.x != 0;

        Anim.SetFloat("yVelocity", Rb.velocity.y);
        Anim.SetBool("isMoving", isMoving);
        Anim.SetBool("isGrounded", IsGrounded);
        Anim.SetBool("isAttacking", IsAttacking);
        Anim.SetInteger("comboCounter", _comboCounter);
    }
}
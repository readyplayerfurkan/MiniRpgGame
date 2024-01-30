using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")] 
    public float playerSpeed;
    public float dashSpeed;
    public float dashDuration;
    public float jumpForce;

    [Header("Collision Info")] 
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    public int FacinDir { get; private set; } = 1;
    private bool _facingRight = true;

    #region Components

    public Animator Anim { get; private set; }
    public Rigidbody2D PlayerRb { get; private set; }

    #endregion
    #region States

    public PlayerStateMachine StateMachine { get; private set; }
    
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerAirState AirState { get; private set; }
    public PlayerDashState DashState { get; private set; }

    #endregion
    
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
    }

    private void Start()
    {
        PlayerRb = GetComponent<Rigidbody2D>();
        Anim = GetComponentInChildren<Animator>();
        
        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        StateMachine.CurrentState.Update();
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        PlayerRb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }

    public bool IsGroundDetected()
    => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void Flip()
    {
        FacinDir *= -1;
        _facingRight = !_facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipController(float xVelocity)
    {
        if (xVelocity > 0 && !_facingRight)
            Flip();
        else if (xVelocity < 0 && _facingRight)
            Flip();
    }
}

using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move Info")] 
    public float playerSpeed;
    
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

    #endregion
    
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        AirState = new PlayerAirState(this, StateMachine, "Jump");
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
    }
}

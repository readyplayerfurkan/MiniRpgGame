using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine PlayerStateMachine;
    protected Player Player;
    protected Rigidbody2D PlayerRb;
    protected float XInput;
    protected float StateTimer;
    
    private string _animBoolName;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, string animBoolName)
    {
        this.Player = player;
        this.PlayerStateMachine = playerStateMachine;
        this._animBoolName = animBoolName;
    }

    public virtual void EnterState()
    {
        Player.Anim.SetBool(_animBoolName, true);
        Debug.Log("I enter " + _animBoolName);
        PlayerRb = Player.PlayerRb;
    }

    public virtual void Update()
    {
        XInput = Input.GetAxis("Horizontal");
        StateTimer -= Time.deltaTime;
        Player.Anim.SetFloat("yVelocity", PlayerRb.velocity.y);
        Debug.Log("I am in " + _animBoolName);
    }

    public virtual void ExitState()
    {
        Player.Anim.SetBool(_animBoolName, false);
        Debug.Log("I exit " + _animBoolName);
    }
}

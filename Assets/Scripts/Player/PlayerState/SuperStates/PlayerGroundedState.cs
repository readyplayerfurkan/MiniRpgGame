using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player,
        playerStateMachine, animBoolName)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void Update()
    {
        base.Update();
        
        if(Input.GetKeyDown(KeyCode.LeftShift))
            Player.StateMachine.ChangeState(Player.DashState);

        if (Input.GetKeyDown(KeyCode.Space) && Player.IsGroundDetected())
            Player.StateMachine.ChangeState(Player.JumpState);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

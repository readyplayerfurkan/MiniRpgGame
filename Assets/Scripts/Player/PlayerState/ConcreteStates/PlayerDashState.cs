using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();

        StateTimer = Player.dashDuration;
    }

    public override void Update()
    {
        base.Update();
        
        Player.SetVelocity(Player.FacinDir * Player.dashSpeed, PlayerRb.velocity.y);
        
        if(StateTimer < 0)
            Player.StateMachine.ChangeState(Player.IdleState);
    }

    public override void ExitState()
    {
        base.ExitState();
        
        Player.SetVelocity(0, PlayerRb.velocity.y);
    }
}

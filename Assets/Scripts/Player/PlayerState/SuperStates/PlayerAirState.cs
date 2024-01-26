using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void Update()
    {
        base.Update();
        
        if (Player.PlayerRb.velocity.y == 0)
            Player.StateMachine.ChangeState(Player.IdleState);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

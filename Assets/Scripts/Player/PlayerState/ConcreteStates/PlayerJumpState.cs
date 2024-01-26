using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        PlayerRb.velocity = new Vector2(PlayerRb.velocity.x, Player.jumpForce);
    }

    public override void Update()
    {
        base.Update();
        
        if (PlayerRb.velocity.y < 0)
            PlayerStateMachine.ChangeState(Player.AirState);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

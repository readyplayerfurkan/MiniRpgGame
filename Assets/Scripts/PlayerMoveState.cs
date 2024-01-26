public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState( Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base( player, playerStateMachine, animBoolName)
    {
        
    } 

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void Update()
    {
        base.Update();
        
        Player.SetVelocity(XInput * Player.playerSpeed, PlayerRb.velocity.y);
        
        if (XInput == 0)
            PlayerStateMachine.ChangeState(Player.IdleState);     
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

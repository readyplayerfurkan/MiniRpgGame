

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState( Player player, PlayerStateMachine playerStateMachine, string animBoolName) : base(player, playerStateMachine, animBoolName)
    {
        
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void Update()
    {
        base.Update();

        if (XInput != 0)
            PlayerStateMachine.ChangeState(Player.MoveState);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}

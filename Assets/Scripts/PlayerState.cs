public class PlayerState
{
    protected PlayerStateMachine PlayerStateMachine;
    protected Player Player;

    private string _animBoolName;

    public PlayerState(PlayerStateMachine playerStateMachine, Player player, string animBoolName)
    {
        this.PlayerStateMachine = playerStateMachine;
        this.Player = player;
        this._animBoolName = animBoolName;
    }

    public virtual void EnterState()
    {
        
    }

    public virtual void Update()
    {
        
    }

    public virtual void ExitState()
    {
        
    }
}

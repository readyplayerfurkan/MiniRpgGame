using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine PlayerStateMachine;
    protected Player Player;

    private string _animBoolName;

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, string animBoolName)
    {
        this.Player = player;
        this.PlayerStateMachine = playerStateMachine;
        this._animBoolName = animBoolName;
    }

    public virtual void EnterState()
    {
        Debug.Log("I enter " + _animBoolName);
    }

    public virtual void Update()
    {
        Debug.Log("I am in " + _animBoolName);
    }

    public virtual void ExitState()
    {
        Debug.Log("I exit " + _animBoolName);
    }
}

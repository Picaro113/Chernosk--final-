using UnityEngine;

public class AttackState : IState
{
    private Searchers searchers;

    public AttackState(Searchers searchers)
    {
        this.searchers = searchers;
    }
    public void Enter()
    {

    }
    public void Update()
    {
        searchers.shoot();

        if (searchers.loseplayertime > 0)
        {
            searchers.seenEnemy = false;
        }

        if (searchers.seenEnemy == false)
        {
            searchers.stateMachine.TransitionTo(searchers.stateMachine.patrollState);
        }
    }

    public void Exit()
    {

    }
}

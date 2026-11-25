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
        //searchers.shoot();
        searchers.Chase();
        if (searchers.seenEnemy == true)
        {
            searchers.Something();
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

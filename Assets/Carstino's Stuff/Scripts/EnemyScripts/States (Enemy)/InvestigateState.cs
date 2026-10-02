using UnityEngine;

public class InvestigateState : IState
{
    private Searchers searchers;

    public InvestigateState(Searchers searchers)
    {
        this.searchers = searchers;
    }
    public void Enter()
    {

    }
    public void Update()
    {
        if (searchers.hunger <= 50 && searchers.seenEnemy == false)
        {
            searchers.closestObject();
            return;
        }
        if (searchers.hunger > 50 && searchers.seenEnemy == false)
        {
            searchers.stateMachine.TransitionTo(searchers.stateMachine.patrollState);
            return;
        }
        if (searchers.seenEnemy == true)
        {
            searchers.stateMachine.TransitionTo(searchers.stateMachine.attackState);
            return;
        }
    }

    public void Exit()
    {

    }
}

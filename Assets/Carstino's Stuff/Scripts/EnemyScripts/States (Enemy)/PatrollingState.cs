using UnityEngine;

public class PatrollingState : IState
{
    private Searchers searchers;

    public PatrollingState(Searchers searchers)
    {
        this.searchers = searchers;
    }
    public void Enter()
    {

    }
    public void Update()
    {
        if (searchers.agent.remainingDistance <= searchers.agent.stoppingDistance)
        {
            searchers.RandomPoint(searchers.planeVector.position, 25, out searchers.point);
            searchers.agent.SetDestination(searchers.point);
            return;
        }
        if (searchers.hunger < 50)
        {
            searchers.stateMachine.TransitionTo(searchers.stateMachine.investigateState);
            return;
        }

    }

    public void Exit()
    {

    }
}

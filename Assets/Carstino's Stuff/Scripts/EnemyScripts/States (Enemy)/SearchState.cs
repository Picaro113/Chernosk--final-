using System.Drawing;
using UnityEngine;

public class SearchState : IState
{
    private Searchers searchers;

    public SearchState(Searchers searchers)
    {
        this.searchers = searchers;
    }
    public void Enter()
    {

    }
    public void Update()
    {
        if (searchers.hunger > 50)
        {
            if (searchers.agent.remainingDistance <= searchers.agent.stoppingDistance)
            {
                searchers.RandomPoint(searchers.planeVector.position, 25, out searchers.point);
                searchers.agent.SetDestination(searchers.point);
                return;
            }
            if (searchers.agent.remainingDistance == searchers.agent.stoppingDistance)
            {
                searchers.stateMachine.TransitionTo(searchers.stateMachine.hideState);
                return;
            }
        }
        if (searchers.hunger < 50)
        {
            searchers.stateMachine.TransitionTo(searchers.stateMachine.foodState);
            return;
        }
    }

    public void Exit()
    {

    }
}

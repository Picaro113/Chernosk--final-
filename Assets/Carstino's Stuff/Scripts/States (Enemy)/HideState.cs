using UnityEngine;

public class HideState : IState
{
    private Searchers searchers;

    public HideState(Searchers searchers)
    {
        this.searchers = searchers;
    }

    public void Enter()
    {

    }
    public void Update()
    {
        if(searchers.hunger > 50)
        {
            searchers.FindNearestHideableObject();
            return;
        }
        else
        {
            searchers.stateMachine.TransitionTo(searchers.stateMachine.searchState);
            return;
        }
    }

    public void Exit()
    {

    }
}

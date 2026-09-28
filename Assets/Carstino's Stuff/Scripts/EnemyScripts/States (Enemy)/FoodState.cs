using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class FoodState : IState
{
    private Searchers searchers;

    public FoodState(Searchers searchers)
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
            searchers.stateMachine.TransitionTo(searchers.stateMachine.searchState);
            return;
        }
        Debug.Log("he's always hungry");
        searchers.closestObject();
    }

    public void Exit()
    {

    }
}

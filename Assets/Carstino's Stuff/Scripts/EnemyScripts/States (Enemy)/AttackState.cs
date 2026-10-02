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
        //do an attack function right here
        searchers.shoot();
    }

    public void Exit()
    {

    }
}

using System;

[Serializable]
public class MainEnemyState
{
    public IState CurrentState { get; private set; }

    public FoodState foodState;
    public SearchState searchState;
    public HideState hideState;



    public AttackState attackState;
    public FlankState flankState;
    public InvestigateState investigateState;
    public PatrollingState patrollState;
    public RetreatState retreatState;

    public MainEnemyState(Searchers searchers)
    {
        foodState = new FoodState(searchers);
        searchState = new SearchState(searchers);
        hideState = new HideState(searchers);

        attackState = new AttackState(searchers);
        flankState = new FlankState(searchers);
        investigateState = new InvestigateState(searchers);
        patrollState = new PatrollingState(searchers);
        retreatState = new RetreatState(searchers);
    }

    public void Initialize(IState startingState)
    {
        CurrentState = startingState;
        startingState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}

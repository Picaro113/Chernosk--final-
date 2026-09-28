using System;

[Serializable]
public class MainEnemyState
{
    public IState CurrentState { get; private set; }

    public FoodState foodState;
    public SearchState searchState;
    public HideState hideState;

    public MainEnemyState(Searchers searchers)
    {
        foodState = new FoodState(searchers);
        searchState = new SearchState(searchers);
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

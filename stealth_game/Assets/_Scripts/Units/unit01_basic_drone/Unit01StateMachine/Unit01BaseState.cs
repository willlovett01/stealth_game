

public abstract class Unit01BaseState {

    protected Unit01StateMachine ctx;
    protected Unit01StateFactory factory;
    public Unit01BaseState(Unit01StateMachine currentContext, Unit01StateFactory unit01StateFactory) {
        ctx = currentContext;
        factory = unit01StateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitialiseSubState();



    protected void UpdateStates() {}

    protected void SwitchState(Unit01BaseState newState) {
        // exit current state
        ExitState();

        // new state enters
        newState.EnterState();

        // switch current state 
        ctx.CurrentState = newState;
    }

    protected void SetSuperState() {}

    protected void SetSubState() {}
}

  
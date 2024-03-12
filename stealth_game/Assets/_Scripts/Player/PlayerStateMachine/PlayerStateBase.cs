
public abstract class PlayerStateBase {

    protected PlayerStateMachine ctx;
    protected PlayerStateFactory factory;
    protected PlayerStateBase currentSubState;
    protected PlayerStateBase currentSuperState;

    public PlayerStateBase(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) {
       ctx = currentContext;
       factory = playerStateFactory;
    }

    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchState();

    public abstract void InitialiseSubState();

    void UpdateStates() { }

    protected void SwitchState(PlayerStateBase newState) { 

        // exit current state
        ExitState();

        //enter new state
        newState.EnterState();

        ctx.CurrentState = newState;
    }

    protected void SetSuperState(PlayerStateBase newSuperState) { 
        currentSuperState = newSuperState;
    }

    protected void SetSubState (PlayerStateBase newSubState) { 
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }


}

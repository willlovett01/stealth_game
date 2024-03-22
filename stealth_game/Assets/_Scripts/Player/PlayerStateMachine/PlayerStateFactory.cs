
public class PlayerStateFactory {

    PlayerStateMachine context;

    public PlayerStateFactory(PlayerStateMachine currentContext) {
        context = currentContext;
    }

    public PlayerStateBase Idle() {
        return new PlayerStateIdle(context, this);
    }

    public PlayerStateBase Walking() {
        return new PlayerStateWalking(context, this);
    }

}


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

    public PlayerStateBase Sneaking() {
        return new PlayerStateSneaking(context, this);
    }
}

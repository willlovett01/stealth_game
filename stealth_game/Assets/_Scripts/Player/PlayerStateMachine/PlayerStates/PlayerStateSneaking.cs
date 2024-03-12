using UnityEngine;

public class PlayerStateSneaking : PlayerStateBase {

    public PlayerStateSneaking(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory) 
    : base (currentContext, playerStateFactory) { }

    public override void EnterState() { }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override void CheckSwitchState() { }

    public override void InitialiseSubState() { }
}

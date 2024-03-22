using UnityEngine;



public class PlayerStateIdle : PlayerStateBase {

    public PlayerStateIdle(PlayerStateMachine currentContext, PlayerStateFactory playerStateFactory)
    : base(currentContext, playerStateFactory) { }

    public override void EnterState() {
        ctx.PlayerNoiseLevel = 3;
        ctx.IsMoving = false;
    }

    public override void UpdateState() {
        CheckSwitchState();
        CheckForSneaking();
    }

    public override void ExitState() { }

    public override void CheckSwitchState() { 

    // check if player movement has been requested
    if(ctx.IsMoveRequested == true) {
            SwitchState(factory.Walking());
        }
    }

    public override void InitialiseSubState() { }

    void CheckForSneaking() {
        if (ctx.IsStealth) {
            ctx.PlayerNoiseLevel = 0;
        }

        else { 
            ctx.PlayerNoiseLevel = 3;
        }
    }
}



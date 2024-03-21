public class Unit01StateFactory {

    Unit01StateMachine context;

    public Unit01StateFactory( Unit01StateMachine currentContext) {
        context = currentContext;
    }

    public Unit01BaseState Patrolling() {
        return new Unit01StatePatrolling(context, this);
    }

    public Unit01BaseState Investigating() {
        return new Unit01StateInvestigating(context, this);
    }

    public Unit01BaseState Chasing() {
        return new Unit01StateChasing(context, this);
    }

    public Unit01BaseState Stunned() {
        return new Unit01StateStunned(context, this);
    }

    public Unit01BaseState Dead() {
        return new Unit01StateDead(context, this);
    }
}


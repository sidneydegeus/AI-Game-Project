
using UnityEngine;

public abstract class SteeringBehaviour : IEntityBehaviour {

    // steering behaviours make use of moving entity, not base entity
    protected MovingEntity entity;

    public SteeringBehaviour(MovingEntity _entity) {
        entity = _entity;
    }

    public abstract void Init();
    public abstract ActionEnum Process();
}
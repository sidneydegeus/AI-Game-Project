using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAction : Action {

    // in all atomics, make use of strategy pattern
    public WanderAction() : base() { }
    public WanderAction(BaseEntity _entity) : base(_entity) {
        Description = "Wander (A)";
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
        entity.entityBehaviours[BehaviourEnum.WANDER_BEHAVIOUR].Init();
    }

    public override void Terminate() {
        Debug.Log("Terminating action");
    }

    public override ActionEnum Process() {        
        Status = entity.entityBehaviours[BehaviourEnum.WANDER_BEHAVIOUR].Process();
        return Status;
    }
/*
    MovingEntity.WanderBehaviour wanderBehaviour;

    public Wander(Human _entity) : base(_entity) {
        Description = "Wander (A)"; 
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
        wanderBehaviour = new MovingEntity.WanderBehaviour(entity);
    }

    public override void Terminate() {
        throw new NotImplementedException();
    }

    public override ActionEnum Process() {
    
        if (entity.WanderStatus() == ActionEnum.STATUS_FAILED) {
            Debug.Log("im wandering and failed");
            Status = ActionEnum.STATUS_FAILED;
            return Status;
        }
        Status = wanderBehaviour.Execute();
        //Debug.Log("processing: " + Description);
        return Status;
    }
	*/
}

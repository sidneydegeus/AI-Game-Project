using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wander : Action {

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
}

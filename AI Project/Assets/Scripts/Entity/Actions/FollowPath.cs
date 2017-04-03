
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

class FollowPath : Action {

    Transform target;

    public FollowPath(MovingEntity _entity, Transform _target) : base(_entity) {
        Description = "Following path to target";
        target = _target;
        Weight = 10;
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
        //Debug.Log("im getting activated");
        entity.RequestPathToTarget(target);
    }

    public override ActionEnum Process() {
        Status = entity.ExecuteFollowPath();
        return Status;
    }

    public override void Terminate() {
        Status = ActionEnum.STATUS_FAILED;
    }
}


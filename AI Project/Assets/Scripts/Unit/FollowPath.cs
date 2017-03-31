using Assets.Scripts.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

class FollowPath : Action {

    Transform target;

    public FollowPath(MovingEntity _unit, Transform _target) : base(_unit) {
        Description = "Following path to target";
        target = _target;
        Weight = 10;
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
        //Debug.Log("im getting activated");
        unit.RequestPathToTarget(target);
    }

    public override ActionEnum Process() {
        if (unit.ExecuteFollowPath()) {
            Status = ActionEnum.STATUS_COMPLETED;
        }
        return Status;
    }

    public override void Terminate() {
        throw new NotImplementedException();
    }
}



using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

class FollowPath : Action {

    Vector3 target;
    MovingEntity.FollowPathBehaviour followPathBehaviour;

    public FollowPath(Human _entity, Vector3 _target) : base(_entity) {
        Description = "Following path (A)";
        target = _target;
        Weight = 10;
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
        followPathBehaviour = new MovingEntity.FollowPathBehaviour(entity, target);
    }

    public override ActionEnum Process() {
        Status = followPathBehaviour.Execute();
        return Status;
    }

    public override void Terminate() {
        //Status = ActionEnum.STATUS_FAILED;
    }
}


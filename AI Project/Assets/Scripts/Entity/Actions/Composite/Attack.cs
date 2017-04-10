using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Attack : ActionGroup {

    public Attack(Human _entity) : base(_entity) {
        Description = "Attack (C)";
        List<Action> actions = new List<Action>();
        actions.Add(new Shoot(entity));
        actions.Add(new FollowPath(entity, entity.LockedTarget.position));
        AddActions(actions);
    }

    protected override void AdditionalProcess() {
        if (entity.LockedTarget == null) {
            Terminate();
        }
    }
}


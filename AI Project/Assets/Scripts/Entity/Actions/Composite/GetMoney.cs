using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GetMoney : ActionGroup {

    float renewAttack = 1.0f;

    public GetMoney(Human _entity) : base(_entity) {
        Description = "Get Money (C)";
        List<Action> actions = new List<Action>();
        actions.Add(new SeekTarget(entity));
        AddActions(actions);
    }

    protected override void AdditionalProcess() {
        if (entity.visibleTargets.Count == 0) {
            entity.LockedTarget = null;
            if (ActionListCount() != 0) {
                Action action = CurrentAction();
                if (action.GetType() != typeof(SeekTarget)) {
                    AddAction(new SeekTarget(entity));
                }
            }
        }
        if (entity.LockedTarget != null) {
            if (ActionListCount() == 0) {
                AddAction(new Attack(entity));
            } else if (renewAttack == 0) {
                RemoveAction();
                AddAction(new Attack(entity));
                renewAttack = 1.0f;
            }
            renewAttack -= Time.deltaTime;
        }
    }
}


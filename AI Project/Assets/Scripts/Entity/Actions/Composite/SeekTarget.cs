using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SeekTarget : ActionGroup {

    public SeekTarget(BaseEntity _entity) : base(_entity) {
        Description = "Seeking Target (C)";
    }

    protected override void AdditionalProcess() {
        if (entity.Stats.Money > 50) {
            Status = ActionEnum.STATUS_COMPLETED;
            return;
        }

        if (entity.fieldOfView.VisibleTargets.Count == 0) {
            if (ActionListCount() == 0) {
                AddAction(new WanderAction(entity));
            }
            if (CurrentAction().GetType() == typeof(ChaseTarget)) {
                RemoveAction();
            }
        }
        else {
            //pick a random target to chase
            if (ActionListCount() == 0) {
                ChaseRandomTarget();
            } else if (CurrentAction().GetType() != typeof(ChaseTarget)) {
                ChaseRandomTarget();
            }

        }
    }

    void ChaseRandomTarget() {
        entity.Target = entity.fieldOfView.VisibleTargets[UnityEngine.Random.Range(0, entity.fieldOfView.VisibleTargets.Count)];
        AddAction(new ChaseTarget(entity));
    }
}


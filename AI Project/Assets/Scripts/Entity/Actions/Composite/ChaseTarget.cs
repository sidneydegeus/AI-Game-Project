using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class ChaseTarget : ActionGroup {

    public ChaseTarget(BaseEntity _entity) : base(_entity) {
        Description = "ChaseTarget (C)";
        AddAction(new FollowpathAction(entity));
    }

    protected override void AdditionalProcess() {
        if (entity.Target != null) {
            float distance = Vector3.Distance(entity.Target.position, entity.transform.position);
            if (distance <= 5.0f && CurrentAction().GetType() != typeof(LookAtAction)) {
                AddAction(new LookAtAction(entity));
            }
            else if (distance > 5.0f && CurrentAction().GetType() == typeof(LookAtAction)) {
                CurrentAction().Status = ActionEnum.STATUS_COMPLETED;
            }
        }

        entity.CurrentAttackCooldown -= Time.deltaTime;
        if (entity.CurrentAttackCooldown <= 0.0f) {
            AddAction(new AttackAction(entity));
            entity.CurrentAttackCooldown = UnityEngine.Random.Range(entity.MinAttackCooldown, entity.MaxAttackCooldown);
        }

        if (entity.Target == null) {
            Status = ActionEnum.STATUS_COMPLETED;
        }
    }
}



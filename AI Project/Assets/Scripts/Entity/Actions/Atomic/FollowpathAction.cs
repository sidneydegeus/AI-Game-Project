
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class FollowpathAction : Action {

    bool running = false;
    const float recalculatePathTimer = 0.5f;
    float currentCooldown;

    public FollowpathAction() : base() { }
    public FollowpathAction(BaseEntity _entity) : base(_entity) {
        Description = "Following path (A)";
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
        entity.entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR].Init();
        currentCooldown = recalculatePathTimer;
    }

    public override ActionEnum Process() {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0.0f) {
            Activate();
        }

        Status = entity.entityBehaviours[BehaviourEnum.FOLLOW_BEHAVIOUR].Process();

        return Status;
    }

    public override void Terminate() {
        Status = ActionEnum.STATUS_FAILED;
    }
}


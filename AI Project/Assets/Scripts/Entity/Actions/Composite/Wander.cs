﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Wander : ActionGroup {

    public Wander(Human _entity) : base(_entity) {
        Description = "Wander (C)";
        AddAction(new FollowPath(entity, entity.GetRandomWanderTarget()));
    }

    protected override void AdditionalProcess() {
    
        if (entity.WanderStatus() == ActionEnum.STATUS_FAILED) {
            Debug.Log("im wandering and failed");
            Status = ActionEnum.STATUS_FAILED;
        }
        //Debug.Log("processing: " + Description);
    }
}

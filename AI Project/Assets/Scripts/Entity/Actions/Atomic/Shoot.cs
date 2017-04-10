using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Shoot : Action {

    float shootCooldown = 0.00f;

    public Shoot(Human _entity) : base(_entity) {
        Description = "Shoot (A)";
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override ActionEnum Process() {
        if (shootCooldown <= 0.00f && Status == ActionEnum.STATUS_ACTIVE) {
            entity.HumanBehaviour.Attack();
            shootCooldown = 1.0f;
            Status = ActionEnum.STATUS_ONHOLD;
        } else if (shootCooldown <= 0.00f && Status == ActionEnum.STATUS_ONHOLD) {
            Status = ActionEnum.STATUS_ACTIVE;
        }
        shootCooldown -= Time.deltaTime;
        if (entity.LockedTarget == null ) {
            Status = ActionEnum.STATUS_COMPLETED;
        }
        return Status;
    }

    public override void Terminate() {
        Status = ActionEnum.STATUS_CANCELED;
    }
}


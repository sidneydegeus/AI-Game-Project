using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : Action {

    float eatTime = 1.00f;

    public Eat(Human _entity) : base(_entity) {
        Description = "Eat (A)";
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override ActionEnum Process() {
        eatTime -= 0.01f;
        if (eatTime <= 0.00f) {
            entity.HumanBehaviour.Eat();
            Status = ActionEnum.STATUS_COMPLETED;
            Debug.Log("Eating");
        }
        return Status;
    }

    public override void Terminate() {
        //throw new NotImplementedException();
    }
}

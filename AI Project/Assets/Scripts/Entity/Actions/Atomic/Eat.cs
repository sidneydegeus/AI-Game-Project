using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : Action {

    Human human;
    float eatTime = 1.00f;

    public Eat(MovingEntity _entity) : base(_entity) {
        human = (Human)entity;
        Description = "Eat (A)";
    }


    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override ActionEnum Process() {
        eatTime -= 0.01f;
        if (eatTime <= 0.00f) {
            human.HumanBehaviour.Eat();
            Status = ActionEnum.STATUS_COMPLETED;
            Debug.Log("Eating");
        }
        return Status;
    }

    public override void Terminate() {
        //throw new NotImplementedException();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Think : ActionGroup {

    Human human;

    public Think(MovingEntity _entity) : base(_entity) {
        Activate();
        Description = "Thinking (C)";
        human = (Human)entity;
        //GoToGroceryStore();
        //GameObject temp = GameObject.Find("Target");
        //Transform target = temp.GetComponent<Transform>();
        //AddAction(new FollowPath(entity, target.position));
    }

    override
    protected void AdditionalProcess() {

        // whenever there is nothing else to do, go wander
        if (ActionStackSize() == 0) {
            AddAction(new Wander(entity));
        }

        Action action = CurrentAction();
        if (human.Hunger > 5 && action.GetType() != typeof(GoingToEat)) {
            AddAction(new GoingToEat(entity));
        }
    }


}


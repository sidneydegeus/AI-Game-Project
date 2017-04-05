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
        Description = "Thinking";
        human = (Human)entity;
        //PathToGroceryStore();
        //GameObject temp = GameObject.Find("Target");
        //Transform target = temp.GetComponent<Transform>();
        //AddAction(new FollowPath(entity, target.position));
    }

    override
    protected void AdditionalProcess() {
        if (human.Hunger > 50) {
            human.HumanBehaviour.Eat();
        }

        // whenever there is nothing to do, go wander
        if (ActionListSize() == 0) {
            AddAction(new Wander(entity));
        }
    }

    void PathToGroceryStore() {
        AddAction(new GoToGroceryStore(entity));
    }
}


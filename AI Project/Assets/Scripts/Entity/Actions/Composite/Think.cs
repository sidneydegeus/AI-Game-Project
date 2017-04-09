using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Think : ActionGroup {


    public Think(Human _entity) : base(_entity) {
        Activate();
        Description = "Thinking (C)";
        //GoToGroceryStore();
        //GameObject temp = GameObject.Find("Target");
        //Transform target = temp.GetComponent<Transform>();
        //AddAction(new FollowPath(entity, target.position));
    }

    override
    protected void AdditionalProcess() {

        // whenever there is nothing else to do, go wander
        if (ActionListCount() == 0) {
            AddAction(new Wander(entity));
        }

        Action action = CurrentAction();
        if (entity.Hunger > 5 && action.GetType() != typeof(GoingToEat)) {
            AddAction(new GoingToEat(entity));
        }
    }


}


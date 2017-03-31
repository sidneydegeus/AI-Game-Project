using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Unit;
using UnityEngine;

class GoToGroceryStore : ActionGroup {
    public GoToGroceryStore(MovingEntity _unit) : base(_unit) {
        Description = "Go To Grocerystore";
        GameObject temp = GameObject.Find("GroceryStore");
        Transform target = temp.GetComponent<Transform>();
        AddAction(new FollowPath(_unit, target));
    }

    public override ActionEnum Process() {

        // use template pattern to make it more generic?
            if (ActionListSize() > 0) {
                Action action = PerformAction();
                if (action.Status == ActionEnum.STATUS_INACTIVE) {
                    Debug.Log("activating action");
                    action.Activate();
                }
                //Debug.Log("executing action");
                action.Process();
                if (action.Process() == ActionEnum.STATUS_ACTIVE) { // has to be is not complete
                    AddAction(action); // re-add to stack if it's not done yet
                }
            } else {
            this.Status = ActionEnum.STATUS_COMPLETED;
        }

        return Status;
    }
}

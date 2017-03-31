using Assets.Scripts.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Think : ActionGroup {

    public Think(MovingEntity _unit) : base(_unit) {
        Activate();
        Description = "Thinking";
        AddAction(new GoToGroceryStore(_unit));
        GameObject temp = GameObject.Find("Target");
        Transform target = temp.GetComponent<Transform>();
        AddAction(new FollowPath(_unit, target));
    }

    override
    public ActionEnum Process() {
        //StartCoroutine(Thinking());
        Thinking();
        return Status;
    }

    void Thinking() {
        //Debug.Log("im thinking");
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
        }
        else {
            Debug.Log("nothing to do");
            // find something to do? if it will ever get here
        }
        //if decision weight < ?? {
        //
        //        // }
    }

    //IEnumerator Thinking() {
    //    // You will never stop thinking, that's why while true
    //    while(true) {
    //        Debug.Log("im thinking");
    //        if (ActionListSize() > 0) {
    //            PerformAction().Process();
    //        } else {
    //            // find something to do? if it will ever get here
    //        }
    //        //if decision weight < ?? {
    //        //
    //        // }
    //        yield return new WaitForSeconds(1f);
    //    }
    //}
}


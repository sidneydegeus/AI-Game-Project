using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class ActionGroup : Action {

    // using stack, so what is added first becomes last, what is added last, becomes first
    Stack<Action> actionList;

    public ActionGroup(MovingEntity _entity) : base(_entity) {
        actionList = new Stack<Action>();
    }

    override
    public void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    override
    public void Terminate() {
        ClearActionList();
    }

    override
    public ActionEnum Process() {
        if (Status == ActionEnum.STATUS_FAILED) {
            Terminate();
            return Status;
        }
        // making use of template pattern here
        // AdditionalProcess will have more specific actions and such
        // that are defined in classes such as Think
        AdditionalProcess();

        if (ActionListSize() > 0) {
            Action action = PerformAction();
            if (action.Status == ActionEnum.STATUS_INACTIVE) {
                action.Activate();
            }
            //action.Process();
            if (action.Process() == ActionEnum.STATUS_ACTIVE) { // has to be is not complete
                AddAction(action); // re-add to stack if it's not done yet
            }
        }
        else {
            this.Status = ActionEnum.STATUS_COMPLETED;
        }
        return Status;
    }

    protected abstract void AdditionalProcess();

    // adds an action to the list
    protected void AddAction(Action action) {
        actionList.Push(action);
    }

    // removes an action from the list and returns the action
    protected Action PerformAction() {
        return actionList.Pop();
    }

    // clears the entire action list
    protected void ClearActionList() {
        actionList.Clear();
    }

    protected int ActionListSize() {
        return actionList.Count;
    }
}

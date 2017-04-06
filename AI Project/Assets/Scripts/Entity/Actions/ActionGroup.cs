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
            Debug.Log("do i do this?");
            Terminate();
            return Status;
        }
        // check if action is completed or failed and remove it


        // making use of template pattern here
        // AdditionalProcess will have more specific actions and such
        // that are defined in classes such as Think
        AdditionalProcess();

        // TODO:
        // Use peek instead of pop? or use something else than a stack? might gain performance

        if (ActionListSize() > 0) {
            Action action = CurrentAction();
            if (action.Status == ActionEnum.STATUS_INACTIVE) {
                action.Activate();
            }
            action.Process();
            if (action.Status == ActionEnum.STATUS_COMPLETED || action.Status == ActionEnum.STATUS_FAILED) {
                RemoveAction();
            }
        } else {
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
    protected Action RemoveAction() {
        return actionList.Pop();
    }

    protected Action CurrentAction() {
        return actionList.Peek();
    }

    // clears the entire action list
    protected void ClearActionList() {
        actionList.Clear();
    }

    protected int ActionListSize() {
        return actionList.Count;
    }
}

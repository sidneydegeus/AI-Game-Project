using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class ActionGroup : Action {

    // using stack, so what is added first becomes last, what is added last, becomes first
    Stack<Action> actionStack;
    

    public ActionGroup(MovingEntity _entity) : base(_entity) {
        actionStack = new Stack<Action>();
    }

    override
    public void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    override
    public void Terminate() {
        for (int i = 0; i < ActionStackSize(); i++) {
            RemoveAction();
        }
        //ClearActionList();
    }

    override
    public ActionEnum Process() {

        // check if action is completed or failed and remove it


        // making use of template pattern here
        // AdditionalProcess will have more specific actions and such
        // that are defined in classes such as Think
        AdditionalProcess();

        if (ActionStackSize() > 0) {
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

        if (Status == ActionEnum.STATUS_FAILED) {
            Debug.Log("do i do this?");
            Terminate();
            return Status;
        }
        return Status;
    }

    protected abstract void AdditionalProcess();

    // adds an action to the list
    protected void AddAction(Action action) {
        actionStack.Push(action);
    }

    // removes an action from the list and returns the action
    protected Action RemoveAction() {
        Action removedAction = actionStack.Pop();
        removedAction.Terminate();
        removeFromTextList(removedAction);
        return removedAction;
    }

    protected Action CurrentAction() {
        try {
            return actionStack.Peek();
        } catch(Exception e) {
            return null;
        }
    }

    // clears the entire action list
    protected void ClearActionList() {
        actionStack.Clear();
    }

    protected int ActionStackSize() {
        return actionStack.Count;
    }

    void removeFromTextList(Action action) {
        Human human = (Human)entity;
        for (int i = 0; i < human.ActionList.Count; i++) {
            if (action.Description.Equals(human.ActionList[i].Description)) {
                human.ActionList.RemoveAt(i);
                break;
            }
        }
    }

}

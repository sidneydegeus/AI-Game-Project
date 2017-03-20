using Assets.Scripts.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class ActionGroup : Action {

    Stack<Action> actionList;

    public ActionGroup(MonoBehaviour _unit) : base(_unit) {
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

    // adds an action to the list
    protected void AddAction(Action action) {
        actionList.Push(action);
    }

    // removes an action from the list
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

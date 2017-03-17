using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class ActionGroup : Action {

    Stack<Action> actionList;

    override
    public void Activate() {
        actionList = new Stack<Action>();
    }

    override
    public void Terminate() {
        ClearActionList();
    }

    // adds an action to the list
    public void AddAction(Action action) {
        actionList.Push(action);
    }

    // removes an action from the list
    public Action PerformAction() {
        return actionList.Pop();
    }

    public void ClearActionList() {
        actionList.Clear();
    }
}

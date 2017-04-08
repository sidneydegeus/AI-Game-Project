using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GoingToEat : ActionGroup {

    List<Action> localActions;

    public GoingToEat(MovingEntity _entity) : base(_entity) {
        Description = "Going To Eat (C)";
        localActions = new List<Action>();
        localActions.Add(new GoToGroceryStore(entity));
        localActions.Add(new Eat(entity));
        AddToStack();
    }

    override
    protected void AdditionalProcess() {
        //Debug.Log("processing: " + Description);
    }


    void AddToStack() {
        localActions.Reverse();
        foreach (Action action in localActions) {
            AddAction(action);
        }
    }
}

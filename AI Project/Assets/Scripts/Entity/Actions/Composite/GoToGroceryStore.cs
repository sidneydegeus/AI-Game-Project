using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GoToGroceryStore : ActionGroup {

    List<Action> localActions;

    public GoToGroceryStore(MovingEntity _entity) : base(_entity) {
        Description = "Go To Grocerystore (C)";
        localActions = new List<Action>();
        localActions.Add(FindPathToGroceryStore());
        localActions.Add(new EnterStore(entity));
        localActions.Add(new PurchaseItem(entity, new Food()));
        localActions.Add(new ExitStore(entity));
        AddToStack();
    }

    override
    protected void AdditionalProcess() {
        //Debug.Log("processing: " + Description);
    }

    Action FindPathToGroceryStore() {
        GameObject temp = GameObject.Find("GroceryStore");
        Transform target = temp.GetComponent<Transform>();
        return new FollowPath(entity, target.position);
    }

    void AddToStack() {
        localActions.Reverse();
        foreach (Action action in localActions) {
            AddAction(action);
        }
    }
}

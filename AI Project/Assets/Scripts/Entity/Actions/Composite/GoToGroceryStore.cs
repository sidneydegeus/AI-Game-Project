using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GoToGroceryStore : ActionGroup {

    public GoToGroceryStore(Human _entity) : base(_entity) {
        Description = "Go To Grocerystore (C)";
        List<Action> actions = new List<Action>();
        actions.Add(FindPathToGroceryStore());
        actions.Add(new EnterStore(entity));
        actions.Add(new PurchaseItem(entity, new Food()));
        actions.Add(new ExitStore(entity));
        AddActions(actions);
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

}

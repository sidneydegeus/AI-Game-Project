using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GoToGroceryStore : ActionGroup {

    public GoToGroceryStore(BaseEntity _entity) : base(_entity) {
        Description = "Go To Grocerystore (C)";
        List<Action> actions = new List<Action>();
        actions.Add(new ExitStore(entity));
        actions.Add(new PurchaseItem(entity, new Food()));
        actions.Add(new EnterStore(entity));
        actions.Add(FindPathToGroceryStore());
        AddActions(actions);
    }

    override
    protected void AdditionalProcess() {
        //Debug.Log("processing: " + Description);
    }

    Action FindPathToGroceryStore() {
        GameObject temp = GameObject.Find("GroceryStore");
        entity.TargetPosition = temp.GetComponent<Transform>().position;

        return new FollowpathAction(entity);
    }

}
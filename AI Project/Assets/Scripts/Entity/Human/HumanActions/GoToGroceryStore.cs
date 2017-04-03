using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GoToGroceryStore : ActionGroup {
    public GoToGroceryStore(MovingEntity _entity) : base(_entity) {
        Description = "Go To Grocerystore";
        FindPathToGroceryStore();
    }

    override
    protected void AdditionalProcess() {
        //Debug.Log("processing: " + Description);
    }

    void FindPathToGroceryStore() {
        GameObject temp = GameObject.Find("GroceryStore");
        Transform target = temp.GetComponent<Transform>();
        AddAction(new FollowPath(entity, target));
    }
}

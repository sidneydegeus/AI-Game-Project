using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GoingToEat : ActionGroup {

    List<Action> localActions;

    public GoingToEat(BaseEntity _entity) : base(_entity) {
        Description = "Going To Eat (C)";
        List<Action> actions = new List<Action>();
        actions.Add(new Eat(entity));
        actions.Add(new GoToGroceryStore(entity));
        AddActions(actions);
    }

    override
    protected void AdditionalProcess() {
        //Debug.Log("processing: " + Description);
    }



}
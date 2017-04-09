using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class GoingToEat : ActionGroup {

    List<Action> localActions;

    public GoingToEat(Human _entity) : base(_entity) {
        Description = "Going To Eat (C)";
        List<Action> actions = new List<Action>();
        actions.Add(new GoToGroceryStore(entity));
        actions.Add(new Eat(entity));
        AddActions(actions);
    }

    override
    protected void AdditionalProcess() {
        //Debug.Log("processing: " + Description);
    }



}

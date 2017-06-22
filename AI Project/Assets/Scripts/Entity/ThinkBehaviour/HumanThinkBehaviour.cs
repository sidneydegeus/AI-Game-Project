using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HumanThinkBehaviour : IThinkBehaviour {
    private NewHuman human;

    public HumanThinkBehaviour(NewHuman _human) {
        human = _human;
    }

    public Action Process() {

        //if (parasite.target != null) {
        //        if (parasite.think.CurrentAction().GetType() != typeof(ChaseTarget)) {
        //            //FollowpathAction followpathAction = new FollowpathAction(parasite);
        //            ChaseTarget chaseTarget = new ChaseTarget(parasite);
        //            return chaseTarget;
        //        }
        //}
        //else {
        //    if (parasite.think.CurrentAction().GetType() == typeof(ChaseTarget)) {
        //        parasite.think.RemoveAction();
        //        parasite.animator.SetBool("Moving", false);
        //    }
        //}
        return null;
    }
}
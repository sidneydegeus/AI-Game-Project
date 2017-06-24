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
        if (human.LastHitBy != null && human.Target != human.LastHitBy) {
            if (human.Think.CurrentAction().GetType() == typeof(Defending) || human.Think.CurrentAction().GetType() == typeof(SeekTarget)) {
                human.Think.RemoveAction();
            }

            human.Target = human.LastHitBy.transform;
            return human.Think.CurrentAction().GetType() != typeof(Defending) ? new Defending(human) : null;
        }
        if (human.Stats.Hunger > 5 && human.Stats.Money < 50) {
            if (human.Think.CurrentAction().GetType() != typeof(SeekTarget)) {
                //FollowpathAction followpathAction = new FollowpathAction(parasite);
                SeekTarget seekTarget = new SeekTarget(human);
                return seekTarget;
            }
        }
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
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

    public double FuzzyGetDesirability(int hungerVar, double moneyVar)
    {
        FuzzyModule fuzzyModule = new FuzzyModule();

        FuzzyVariable hunger = fuzzyModule.CreateFLV("Hunger");
        FzSet No_Hunger = hunger.AddLeftShoulderSet("No_Hunger", 0, 3, 6);
        FzSet Hunger = hunger.AddTriangularSet("Hunger", 6, 9, 12);
        FzSet Very_Hunger = hunger.AddRightShoulderSet("Very_Hunger", 12, 15, 20);

        FuzzyVariable AmmoStatus = fuzzyModule.CreateFLV("Money");
        FzSet Poor = AmmoStatus.AddLeftShoulderSet("Poor", 0, 5, 10);
        FzSet Normal = AmmoStatus.AddTriangularSet("Normal", 10, 50, 100);
        FzSet Rich = AmmoStatus.AddRightShoulderSet("Rich", 100, 500, 1000);

        FuzzyVariable Desirability = fuzzyModule.CreateFLV("Desirability");
        FzSet Undesirable = Desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);
        FzSet Desirable = Desirability.AddTriangularSet("Desirable", 25, 50, 75);
        FzSet Very_Desirable = Desirability.AddRightShoulderSet("Very_Desirable", 50, 75, 100);

        fuzzyModule.AddRule(new FzAND(No_Hunger, Poor), Undesirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Normal), Undesirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Rich), Undesirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Poor), Very_Desirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Normal), Very_Desirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Rich), Undesirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Poor), Desirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Normal), Desirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Rich), Undesirable);

        fuzzyModule.Fuzzify("Hunger", hungerVar);
        fuzzyModule.Fuzzify("Money", moneyVar);

        double value = fuzzyModule.DeFuzzify("Desirability", FuzzyModule.DefuzzifyMethod.MaxAV);

        return value;
    }

    public Action Process() {
        if (human.LastHitBy != null && human.Target != human.LastHitBy) {
            if (human.Think.CurrentAction().GetType() == typeof(Defending) || human.Think.CurrentAction().GetType() == typeof(SeekTarget)) {
                human.Think.RemoveAction();
            }

            human.Target = human.LastHitBy.transform;
            return human.Think.CurrentAction().GetType() != typeof(Defending) ? new Defending(human) : null;
        }

        //Debug.Log("Fuzzy Logic Desirability: " + FuzzyGetDesirability(human.Stats.Hunger, human.Stats.Money));

        //if (human.Stats.Hunger > 5 && human.Stats.Money < 50) {
        if (FuzzyGetDesirability(human.Stats.Hunger, human.Stats.Money) > 85) {
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
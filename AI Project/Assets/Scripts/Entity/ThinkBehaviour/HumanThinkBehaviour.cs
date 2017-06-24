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

    public double FuzzyGetDesirabilitySeek(int hungerVar, double moneyVar)
    {
        FuzzyModule fuzzyModule = new FuzzyModule();

        FuzzyVariable hunger = fuzzyModule.CreateFLV("Hunger");
        FzSet No_Hunger = hunger.AddLeftShoulderSet("No_Hunger", 0, 5, 10);
        FzSet Hunger = hunger.AddTriangularSet("Hunger", 5, 10, 15);
        FzSet Very_Hunger = hunger.AddRightShoulderSet("Very_Hunger", 10, 15, 20);

        FuzzyVariable AmmoStatus = fuzzyModule.CreateFLV("Money");
        FzSet Poor = AmmoStatus.AddLeftShoulderSet("Poor", 0, 5, 10);
        FzSet Normal = AmmoStatus.AddTriangularSet("Normal", 5, 10, 100);
        FzSet Rich = AmmoStatus.AddRightShoulderSet("Rich", 10, 100, 1000);

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

    public double FuzzyGetDesirabilityEat(int hungerVar, double moneyVar, double healthVar)
    {
        FuzzyModule fuzzyModule = new FuzzyModule();

        FuzzyVariable hunger = fuzzyModule.CreateFLV("Hunger");
        FzSet No_Hunger = hunger.AddLeftShoulderSet("No_Hunger", 0, 5, 10);
        FzSet Hunger = hunger.AddTriangularSet("Hunger", 5, 10, 15);
        FzSet Very_Hunger = hunger.AddRightShoulderSet("Very_Hunger", 10, 15, 20);

        FuzzyVariable AmmoStatus = fuzzyModule.CreateFLV("Money");
        FzSet Poor = AmmoStatus.AddLeftShoulderSet("Poor", 0, 5, 10);
        FzSet Normal = AmmoStatus.AddTriangularSet("Normal", 5, 10, 100);
        FzSet Rich = AmmoStatus.AddRightShoulderSet("Rich", 10, 100, 1000);

        FuzzyVariable health = fuzzyModule.CreateFLV("Health");
        FzSet Almost_Dead = hunger.AddLeftShoulderSet("Almost_Dead", 0, 10, 25);
        FzSet Not_Healthy = hunger.AddTriangularSet("Not_Healthy", 10, 25, 75);
        FzSet Healthy = hunger.AddRightShoulderSet("Healthy", 25, 75, 100);

        FuzzyVariable Desirability = fuzzyModule.CreateFLV("Desirability");
        FzSet Undesirable = Desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);
        FzSet Desirable = Desirability.AddTriangularSet("Desirable", 25, 50, 75);
        FzSet Very_Desirable = Desirability.AddRightShoulderSet("Very_Desirable", 50, 75, 100);

        fuzzyModule.AddRule(new FzAND(Very_Hunger, Rich, Almost_Dead), Undesirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Normal, Almost_Dead), Very_Desirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Poor, Almost_Dead), Undesirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Rich, Not_Healthy), Very_Desirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Normal, Not_Healthy), Very_Desirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Poor, Not_Healthy), Undesirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Rich, Healthy), Desirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Normal, Healthy), Desirable);
        fuzzyModule.AddRule(new FzAND(Very_Hunger, Poor, Healthy), Undesirable);

        fuzzyModule.AddRule(new FzAND(Hunger, Poor, Almost_Dead), Undesirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Normal, Almost_Dead), Very_Desirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Rich, Almost_Dead), Very_Desirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Poor, Not_Healthy), Undesirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Normal, Not_Healthy), Desirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Rich, Not_Healthy), Desirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Poor, Healthy), Undesirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Normal, Healthy), Desirable);
        fuzzyModule.AddRule(new FzAND(Hunger, Rich, Healthy), Desirable);

        fuzzyModule.AddRule(new FzAND(No_Hunger, Poor, Not_Healthy), Undesirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Normal, Not_Healthy), Undesirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Rich, Not_Healthy), Undesirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Poor, Healthy), Undesirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Normal, Healthy), Undesirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Rich, Healthy), Undesirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Poor, Almost_Dead), Undesirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Normal, Almost_Dead), Desirable);
        fuzzyModule.AddRule(new FzAND(No_Hunger, Rich, Almost_Dead), Desirable);

        fuzzyModule.Fuzzify("Hunger", hungerVar);
        fuzzyModule.Fuzzify("Money", moneyVar);
        fuzzyModule.Fuzzify("Health", healthVar);

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

        Debug.Log("Fuzzy Logic Desirability Seek: " + FuzzyGetDesirabilitySeek(human.Stats.Hunger, human.Stats.Money));
        Debug.Log("Fuzzy Logic Desirability Eat: " + FuzzyGetDesirabilityEat(human.Stats.Hunger, human.Stats.Money, human.Stats.Health));

        //if (human.Stats.Hunger > 5 && human.Stats.Money < 50) {
        //if (human.Stats.Hunger > 15 && human.Think.CurrentAction().GetType() != typeof(GoingToEat) && human.Stats.Money >= 50) {
        if (FuzzyGetDesirabilityEat(human.Stats.Hunger, human.Stats.Money, human.Stats.Health) > 85 && human.Think.CurrentAction().GetType() != typeof(GoingToEat))
        {
            return new GoingToEat(human);
        }
        if (FuzzyGetDesirabilitySeek(human.Stats.Hunger, human.Stats.Money) > 85) {
            if (human.Think.CurrentAction().GetType() != typeof(SeekTarget)) {
                SeekTarget seekTarget = new SeekTarget(human);
                return seekTarget;
            }
        }

        return null;
    }
}
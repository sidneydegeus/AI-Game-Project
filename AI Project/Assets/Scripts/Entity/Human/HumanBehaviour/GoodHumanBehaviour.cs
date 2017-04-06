using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodHumanBehaviour : IHumanBehaviour {

    Human human;

    public GoodHumanBehaviour(Human _human) {
        WorldManager.GoodHumanCount++;
        human = _human;
    }

    public void Attack() {
        // a good human doesn't attack, but defends instead
        throw new NotImplementedException();
    }

    public void Eat() {
        // a good human goes to the grocery store to buy food and then eat it
        throw new NotImplementedException();
    }

    public void Purchase(Item item) {
        if (item.GetType() == typeof(Food)) {
            Debug.Log("i just bought food");
            Food food = (Food)item;
            int amountToBuy;
            int foodAmountRequired = human.Hunger / food.HungerValue;
            int amountPossibleToBuy = human.Money / food.Cost;
            if (foodAmountRequired > amountPossibleToBuy) {
                amountToBuy = amountPossibleToBuy;
            } else {
                amountToBuy = foodAmountRequired;
            }
            human.Money -= amountToBuy * food.Cost;
            human.Hunger -= amountToBuy * food.HungerValue;
            if (human.Hunger < 0) {
                human.Hunger = 0;
            }
        }

        // buy weapon here

    }

    public void Rest() {
        // idk just standard rest?
        throw new NotImplementedException();
    }
}

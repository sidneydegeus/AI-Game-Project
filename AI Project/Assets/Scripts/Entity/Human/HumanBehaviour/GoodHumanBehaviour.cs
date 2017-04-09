using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoodHumanBehaviour : IHumanBehaviour {

    private string description;
    Human human;

    public GoodHumanBehaviour(Human _human) {
        WorldManager.GoodHumanCount++;
        human = _human;
        Description = "Good";
    }

    public string Description {
        get {
            return description;
        }

        set {
            description = value;
        }
    }

    public void Attack() {
        // a good human doesn't attack, but defends instead
        throw new NotImplementedException();
    }

    public void Eat() {
        foreach (Item item in human.Inventory.ToList()) {
            if (item.GetType() == typeof(Food)) {
                Food food = (Food)item;
                human.Hunger -= food.HungerValue;
                if (human.Hunger < 0) {
                    human.Hunger = 0;
                }
                human.Inventory.Remove(food);
            }
        }
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
            for (int i = 0; i < amountToBuy; i++) {
                human.Inventory.Add(food);
            }
        }

        // buy weapon here

    }

    public void Rest() {
        // idk just standard rest?
        throw new NotImplementedException();
    }

    public void Tick() {
        human.Money += 5;
        human.Hunger += 1;
        if (human.Hunger >= 100) {
            human.Health -= 1;
        }
        else if (human.Hunger <= 50) {
            human.Health += 1;
            if (human.Health > 100) {
                human.Health = 100;
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Eat : Action {

    float eatTime = 1.00f;

    public Eat(BaseEntity _entity) : base(_entity) {
        Description = "Eat (A)";
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override ActionEnum Process() {
        eatTime -= 0.01f;
        if (eatTime <= 0.00f) {
            foreach (Item item in entity.Inventory.ToList()) {
                if (item.GetType() == typeof(Food)) {
                    Food food = (Food)item;
                    entity.Stats.Hunger -= food.HungerValue;
                    if (entity.Stats.Hunger < 0) {
                        entity.Stats.Hunger = 0;
                    }
                    entity.Inventory.Remove(food);
                }
            }
            Status = ActionEnum.STATUS_COMPLETED;
            Debug.Log("Eating");
        }
        return Status;
    }

}
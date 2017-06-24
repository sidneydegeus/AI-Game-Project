using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class PurchaseItem : Action {

    Item item;
    float buyingItem;

    public PurchaseItem(BaseEntity _entity, Item _item) : base(_entity) {
        item = _item;
        buyingItem = 1.00f;
        Description = "Purchase (" + item.Name + ") (A) ";
    }

    public override void Activate() {
        Status = ActionEnum.STATUS_ACTIVE;
    }

    public override ActionEnum Process() {
        buyingItem -= 0.01f;
        if (buyingItem <= 0.00f) {
            if (item.GetType() == typeof(Food)) {
                Debug.Log("i just bought food");
                Food food = (Food)item;
                int amountToBuy;
                int foodAmountRequired = entity.Stats.Hunger / food.HungerValue;
                int amountPossibleToBuy = (int)Math.Round(entity.Stats.Money / food.Cost);
                if (foodAmountRequired > amountPossibleToBuy) {
                    amountToBuy = amountPossibleToBuy;
                }
                else {
                    amountToBuy = foodAmountRequired;
                }
                entity.Stats.Money -= amountToBuy * food.Cost;
                for (int i = 0; i < amountToBuy; i++) {
                    entity.Inventory.Add(food);
                }
            }
            Status = ActionEnum.STATUS_COMPLETED;
        }
        return Status;
    }

    public override void Terminate() {
        //throw new NotImplementedException();
    }
}
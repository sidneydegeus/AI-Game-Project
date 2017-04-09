﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class PurchaseItem : Action {

    Item item;
    float buyingItem;

    public PurchaseItem(Human _entity, Item _item) : base(_entity) {
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
            entity.HumanBehaviour.Purchase(item);
            Status = ActionEnum.STATUS_COMPLETED;
        }
        return Status;
    }

    public override void Terminate() {
        //throw new NotImplementedException();
    }
}


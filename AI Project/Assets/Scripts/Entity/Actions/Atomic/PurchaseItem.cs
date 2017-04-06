using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    class PurchaseItem : Action {

    Human human;
    Item item;
    float buyingItem;

        public PurchaseItem(MovingEntity _entity, Item _item) : base(_entity) {
            Description = "Purchasing item";
            human = (Human)entity;
        item = _item;
        buyingItem = 1.00f;
    }

        public override void Activate() {
            Status = ActionEnum.STATUS_ACTIVE;
        }

        public override ActionEnum Process() {
            buyingItem -= 0.02f;
            if (buyingItem <= 0.00f) {
                human.HumanBehaviour.Purchase(item);
                Status = ActionEnum.STATUS_COMPLETED;
            }
            return Status;
        }

        public override void Terminate() {
            throw new NotImplementedException();
        }
    }

